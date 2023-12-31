using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Helpers;
using Web_CuaHangCafe.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Web_CuaHangCafe.Controllers
{
    public class ShopCartController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");

                if (data == null)
                {
                    data = new List<CartItem>();
                }

                return data;
            }
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("GioHang");

            if (cartItems != null && cartItems.Any())
            {
                decimal? tongTien = cartItems.Sum(p => p.DonGia * p.SoLuong);
                string totalCart = tongTien.Value.ToString("n0");
                ViewData["total"] = totalCart;
                return View(Carts);
            }
            else
            {
                ViewData["total"] = "0";
                return View(Carts);
            }
        }

        public IActionResult AddToCart(int id, int quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.MaSp == id);
            decimal? tongTien = 0;

            if (item == null)
            {
                var hangHoa = db.TbSanPhams.SingleOrDefault(p => p.MaSanPham == id);

                item = new CartItem
                {
                    MaSp = id,
                    TenSp = hangHoa.TenSanPham,
                    DonGia = hangHoa.GiaBan.Value,
                    SoLuong = quantity,
                    AnhSp = hangHoa.HinhAnh
                };

                myCart.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            

            HttpContext.Session.Set("GioHang", myCart);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveToCart(int id)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.MaSp == id);

            if (item != null)
            {
                // Đã tồn tại, tăng thêm 1
                myCart.Remove(item);
            }

            decimal? tongTien = myCart.Sum(p => p.DonGia * p.SoLuong);
            string totalCart = tongTien.Value.ToString("n0");
            TempData["total"] = totalCart;

            HttpContext.Session.Set("GioHang", myCart);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout(string customerName, string phoneNumber, string address)
        {
            // Lấy thông tin giỏ hàng từ session
            var cartItems = HttpContext.Session.Get<List<CartItem>>("GioHang");

            Random random = new Random();
            Guid orderId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();

            // Kiểm tra xem giỏ hàng có sản phẩm không
            if (cartItems != null && cartItems.Any())
            {
                var _customer = new TbKhachHang
                {
                    Id = customerId,
                    TenKhachHang = customerName,
                    SdtkhachHang = phoneNumber,
                    DiaChi = address
                };

                db.TbKhachHangs.Add(_customer);
                db.SaveChanges();

                // Tạo hoá đơn
                var order = new TbHoaDonBan
                {
                    MaHoaDon = orderId,
                    SoHoaDon = random.Next(1, 100).ToString(),
                    NgayBan = DateTime.Now,
                    TongTien = cartItems.Sum(p => p.DonGia * p.SoLuong),
                    CustomerId = customerId
                };

                db.TbHoaDonBans.Add(order);
                db.SaveChanges();

                foreach (var cartItem in cartItems)
                {
                    var orderDetails = new TbChiTietHoaDonBan
                    {
                        MaHoaDon = orderId,
                        MaSanPham = cartItem.MaSp,
                        GiaBan = cartItem.DonGia,
                        GiamGia = 0,
                        SoLuong = cartItem.SoLuong,
                        ThanhTien = cartItem.ThanhTien
                    };

                    db.TbChiTietHoaDonBans.Add(orderDetails);
                    db.SaveChanges();
                }

                // Xóa giỏ hàng sau khi tạo hoá đơn
                HttpContext.Session.Remove("GioHang");

                // Chuyển hướng đến trang xác nhận thanh toán
                return RedirectToAction("Confirmation");
            }
            else
            {
                // Giỏ hàng rỗng, thực hiện hành động tương ứng (ví dụ: hiển thị thông báo)
                return RedirectToAction("EmptyCart");
            }
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult EmptyCart()
        {
            return View();
        }

        // Phương thức cập nhật số lượng sản phẩm trong giỏ hàng
        //[HttpPost]
        //public IActionResult UpdateCartItem(string id, int quantity)
        //{
        //    // Lấy thông tin sản phẩm từ cơ sở dữ liệu hoặc từ nơi lưu trữ sản phẩm
        //    var myCart = Carts;
        //    var item = myCart.SingleOrDefault(p => p.MaSp == id);

        //    if (item != null)
        //    {
        //        // Lấy thông tin giỏ hàng từ Session (nếu đã tồn tại)
        //        List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("GioHang") ?? new List<CartItem>();

        //        // Tìm sản phẩm trong giỏ hàng
        //        CartItem existingCartItem = myCart.FirstOrDefault(item => item.MaSp == id);

        //        if (existingCartItem != null)
        //        {
        //            // Cập nhật số lượng sản phẩm
        //            existingCartItem.SoLuong = quantity;

        //            // Cập nhật giá trị sản phẩm trong giỏ hàng
        //            existingCartItem.DonGia = item.DonGia * quantity;

        //            // Cập nhật thông tin giỏ hàng trong Session
        //            HttpContext.Session.Set("GioHang", myCart);
        //        }
        //    }

        //    // Chuyển hướng người dùng trở lại trang giỏ hàng
        //    return RedirectToAction("Index", "GioHang");
        //}

        //public decimal CalculateTotalPrice()
        //{
        //    List<CartItem> cartItems = HttpContext.Session.Get<List<CartItem>>("GioHang") ?? new List<CartItem>();
        //    decimal totalPrice = 0;
        //    foreach (var cartItem in cartItems)
        //    {
        //        totalPrice += cartItem.DonGia * cartItem.SoLuong;
        //    }
        //    return totalPrice;
        //}
        ///

        //// Action để hiển thị giỏ hàng
        //public IActionResult Index()
        //{
        //    var cartItems = GetCartItems();
        //    return View(cartItems);
        //}

        //// Action để thêm sản phẩm vào giỏ hàng
        //[HttpPost]
        //public IActionResult AddToCart(int id, int quantity)
        //{
        //    var productItem = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == id);

        //    var item = new CartItem
        //    {
        //        ProductId = id,
        //        ProductName = productItem.TenSanPham,
        //        Price = productItem.GiaBan,
        //        ProductImage = productItem.HinhAnh,
        //        Quantity = quantity
        //    };

        //    AddItemToCart(item);

        //    // Chuyển hướng về trang giỏ hàng
        //    return RedirectToAction("Index");
        //}

        //// Action để xóa sản phẩm khỏi giỏ hàng
        //public IActionResult RemoveFromCart(int productId)
        //{
        //    RemoveItemFromCart(productId);
        //    return RedirectToAction("Index");
        //}

        //// Phương thức để lấy giỏ hàng từ Session
        //private List<CartItem> GetCartItems()
        //{
        //    var sessionCart = HttpContext.Session.Get<List<CartItem>>("Cart");
        //    return sessionCart ?? new List<CartItem>();
        //}

        //// Phương thức để thêm sản phẩm vào giỏ hàng trong Session
        //private void AddItemToCart(CartItem item)
        //{
        //    var cart = GetCartItems();

        //    var existingItem = cart.FirstOrDefault(i => i.ProductId == item.ProductId);

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += item.Quantity;
        //    }
        //    else
        //    {
        //        cart.Add(item);
        //    }

        //    SaveCart(cart);
        //}

        //// Phương thức để xóa sản phẩm khỏi giỏ hàng trong Session
        //private void RemoveItemFromCart(int productId)
        //{
        //    var cart = GetCartItems();
        //    var itemToRemove = cart.FirstOrDefault(i => i.ProductId == productId);

        //    if (itemToRemove != null)
        //    {
        //        cart.Remove(itemToRemove);
        //        SaveCart(cart);
        //    }
        //}

        //// Phương thức để lưu giỏ hàng vào Session
        //private void SaveCart(List<CartItem> cart)
        //{
        //    HttpContext.Session.Set("Cart", cart);
        //}
    }
}
