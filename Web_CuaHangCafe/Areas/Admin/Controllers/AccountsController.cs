using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Accounts")]
    public class AccountsController : Controller
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

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbQuanTriViens.AsNoTracking().OrderBy(x => x.TenNguoiDung).ToList();
            PagedList<TbQuanTriVien> pagedListItem = new PagedList<TbQuanTriVien>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbQuanTriVien quanTriVien)
        {
            db.TbQuanTriViens.Add(quanTriVien);
            db.SaveChanges();
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "Accounts");
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            var quanTriVien = db.TbQuanTriViens.Find(id);
            ViewBag.id = id;
            return View(quanTriVien);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbQuanTriVien quanTriVien)
        {
            string hashPass = HashPassword(quanTriVien.MatKhau);
            quanTriVien.MatKhau = hashPass;
            db.Entry(quanTriVien).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Sửa thành công";
            return RedirectToAction("Index", "Accounts");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";
            db.Remove(db.TbQuanTriViens.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index", "Accounts");
        }
    }
}
