using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Controllers
{
    public class AccessController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the SHA-256 hash of the password bytes
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

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
            string hashPassword = HashPassword(user.MatKhau);

            if (HttpContext.Session.GetString("TenNguoiDung") == null)
            {
                var u = db.TbQuanTriViens.Where(x => x.TenNguoiDung.Equals(user.TenNguoiDung) && x.MatKhau.Equals(hashPassword)).FirstOrDefault();

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
