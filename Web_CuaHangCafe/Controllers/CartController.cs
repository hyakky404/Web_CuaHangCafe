using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Helpers;
using Web_CuaHangCafe.Data;

namespace Web_CuaHangCafe.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");

                if (data == null)
                {
                    data = new List<CartItem>();
                }

                return data;
            }
        }

        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (cartItems != null && cartItems.Any())
            {
                decimal? tongTien = cartItems.Sum(p => p.DonGia * p.SoLuong);
                string totalCart = tongTien.Value.ToString("n0");
                ViewData["total"] = totalCart;
                return View(Carts);
            }
            else
            {
                ViewData["Message"] = "Không có sản phẩm trong giỏ hàng.";
                ViewData["total"] = "0";
                return View(Carts);
            }
        }

        public IActionResult Add(int id, int quantity)
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

            

            HttpContext.Session.Set("Cart", myCart);
            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Update([FromBody] List<UpdateQuantityRequest> updates)
        {
            if (updates == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid request.");
            }

            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (cartItems != null)
            {
                foreach (var update in updates)
                {
                    var cartItemToUpdate = cartItems.Find(item => item.MaSp == update.ProductId);

                    if (cartItemToUpdate != null)
                    {
                        cartItemToUpdate.SoLuong = update.Quantity;
                    }
                }

                HttpContext.Session.Set("Cart", cartItems);

                decimal? totalAmount = 0;
                foreach (var item in cartItems)
                {
                    totalAmount += item.SoLuong * item.DonGia;
                }

                return Json(new { success = true, message = "Số lượng đã được cập nhật.", totalAmount = totalAmount, cartItems = cartItems });
            }

            return BadRequest("Invalid cart.");
        }

        public class UpdateQuantityRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        public IActionResult Remove(int maSp)
        {
            try
            {
                var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

                if (cartItems != null)
                {
                    var productToRemove = cartItems.SingleOrDefault(item => item.MaSp == maSp);

                    if (productToRemove != null)
                    {
                        cartItems.Remove(productToRemove);

                        HttpContext.Session.Set("Cart", cartItems);

                        decimal? totalAmount = 0;

                        foreach (var item in cartItems)
                        {
                            totalAmount += item.SoLuong * item.DonGia;
                        }

                        return Json(new { success = true, message = "Đã xoá sản phẩm.", totalAmount = totalAmount, cartItems = cartItems });
                    }
                    else
                    {
                        Console.WriteLine(maSp);
                        return Json(new { success = false, message = "Không có sản phẩm." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Giỏ hàng rỗng." });
                }    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");


            if (cartItems != null && cartItems.Any())
            {
                decimal? tongTien = cartItems.Sum(p => p.DonGia * p.SoLuong);
                string totalCart = tongTien.Value.ToString("n0");
                ViewData["total"] = totalCart;
                return View(Carts);
            }
            else
            {
                return RedirectToAction("index", "home"); 
            }
        }

        public IActionResult Confirmation(string customerName, string phoneNumber, string address)
        {
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");

            Random random = new Random();
            Guid orderId = Guid.NewGuid();
            Guid customerId = Guid.NewGuid();

            if (cartItems != null && cartItems.Any())
            {
                var custormer = _context.TbKhachHangs.FirstOrDefault(x => x.SdtkhachHang.Equals(phoneNumber));

                if (custormer == null)
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
                }    
                else
                {
                    custormer.TenKhachHang = customerName;
                    custormer.DiaChi = address;
                    _context.SaveChanges();

                    var order = new TbHoaDonBan
                    {
                        MaHoaDon = orderId,
                        SoHoaDon = random.Next(1, 100).ToString(),
                        NgayBan = DateTime.Now,
                        TongTien = cartItems.Sum(p => p.DonGia * p.SoLuong),
                        CustomerId = custormer.Id
                    };

                    _context.TbHoaDonBans.Add(order);
                    _context.SaveChanges();
                }    

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

                HttpContext.Session.Remove("Cart");

                return RedirectToAction("success");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
