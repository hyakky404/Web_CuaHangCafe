using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Controllers
{
    public class AccessController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TenNguoiDung") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
        }

        [HttpPost]
        public IActionResult Login(TbQuanTriVien user)
        {
            if (HttpContext.Session.GetString("TenNguoiDung") == null)
            {
                var u = db.TbQuanTriViens.Where(x => x.TenNguoiDung.Equals(user.TenNguoiDung) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();

                if (u != null)
                {
                    HttpContext.Session.SetString("TenNguoiDung", u.TenNguoiDung.ToString());
                    return RedirectToAction("Index", "HomeAdmin");
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TenNguoiDung");
            return RedirectToAction("Login", "Access");
        }
    }
}
