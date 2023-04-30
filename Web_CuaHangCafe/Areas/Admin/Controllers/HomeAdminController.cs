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
    [Route("HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [Route("")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbSanPhams.AsNoTracking().OrderBy(x => x.MaSanPham).ToList();
            PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
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
            var listItem = db.TbSanPhams.AsNoTracking().Where(x => x.TenSanPham.ToLower().Contains(search)).OrderBy(x => x.MaSanPham).ToList();
            PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaNhomSp = new SelectList(db.TbNhomSanPhams.ToList(), "MaNhomSp", "TenNhomSp");
            return View();
        }

        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbSanPham sanPham)
        {
            db.TbSanPhams.Add(sanPham);
            db.SaveChanges();
            TempData["Message"] = "Thêm sản phẩm thành công";
            return RedirectToAction("Index", "HomeAdmin");
        }

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(string id, string name)
        {
            var sanPham = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == id);
            ViewBag.name = name;
            return View(sanPham);
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(string id, string name)
        {
            ViewBag.MaNhomSp = new SelectList(db.TbNhomSanPhams.ToList(), "MaNhomSp", "TenNhomSp");
            var sanPham = db.TbSanPhams.Find(id);
            ViewBag.name = name;
            return View(sanPham);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbSanPham sanPham)
        {
            db.Entry(sanPham).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Sửa sản phẩm thành công";
            return RedirectToAction("Index", "HomeAdmin");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";
            var chiTietHoaDon = db.TbChiTietHoaDonBans.Where(x => x.MaSanPham == id).ToList();

            if (chiTietHoaDon.Count() > 0)
            {
                TempData["Message"] = "Không xoá được sản phẩm";
                return RedirectToAction("Index", "HomeAdmin");
            }

            db.Remove(db.TbSanPhams.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xoá";
            return RedirectToAction("Index", "HomeAdmin");
        }
    }
}
