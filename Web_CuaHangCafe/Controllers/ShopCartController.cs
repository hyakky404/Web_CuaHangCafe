using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Helpers;
using Web_CuaHangCafe.Data;

namespace Web_CuaHangCafe.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopCartController(ApplicationDbContext context)
        {
            _context = context;
        }

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
                var hangHoa = _context.TbSanPhams.SingleOrDefault(p => p.MaSanPham == id);

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

                _context.TbKhachHangs.Add(_customer);
                _context.SaveChanges();

                // Tạo hoá đơn
                var order = new TbHoaDonBan
                {
                    MaHoaDon = orderId,
                    SoHoaDon = random.Next(1, 100).ToString(),
                    NgayBan = DateTime.Now,
                    TongTien = cartItems.Sum(p => p.DonGia * p.SoLuong),
                    CustomerId = customerId
                };

                _context.TbHoaDonBans.Add(order);
                _context.SaveChanges();

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

                    _context.TbChiTietHoaDonBans.Add(orderDetails);
                    _context.SaveChanges();
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
    }
}
