using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Clients")]
    public class ClientsController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbKhachHangs.AsNoTracking().OrderBy(x => x.SdtkhachHang).ToList();
            PagedList<TbKhachHang> pagedListItem = new PagedList<TbKhachHang>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        [Route("Search")]
        [Authentication]
        [HttpGet]
        public IActionResult Search(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            search = search.ToLower();
            ViewBag.search = search;
            var listItem = db.TbKhachHangs.AsNoTracking().Where(x => x.TenKhachHang.ToLower().Contains(search)).OrderBy(x => x.SdtkhachHang).ToList();
            PagedList<TbKhachHang> pagedListItem = new PagedList<TbKhachHang>(listItem, pageNumber, pageSize);
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
        public IActionResult Create(TbKhachHang khachHang)
        {
            db.TbKhachHangs.Add(khachHang);
            db.SaveChanges();
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "Clients");
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(string id, string name)
        {
            var khachHang = db.TbKhachHangs.Find(id);
            ViewBag.name = name;
            return View(khachHang);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbKhachHang khachHang)
        {
            db.Entry(khachHang).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Sửa thành công";
            return RedirectToAction("Index", "Clients");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";
            var hoaDon = db.TbHoaDonBans.Where(x => x.MaHoaDon == id).ToList();

            if (hoaDon.Count() > 0)
            {
                TempData["Message"] = "Xoá không thành công";
                return RedirectToAction("Index", "Clients");
            }

            db.Remove(db.TbKhachHangs.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index", "Clients");
        }
    }
}
