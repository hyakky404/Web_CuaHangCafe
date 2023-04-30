using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Helpers;

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
            return View(Carts);
        }

        public IActionResult AddToCart(string id, int quantity)
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

                tongTien += item.DonGia;
                myCart.Add(item);
            }
            else
            {
                tongTien += item.DonGia;
                item.SoLuong += quantity;
            }

            ViewData["total"] = tongTien;

            HttpContext.Session.Set("GioHang", myCart);
            return RedirectToAction("Index");
        }
    }
}
