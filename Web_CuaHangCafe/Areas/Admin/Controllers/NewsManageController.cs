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
    [Route("Admin/News")]
    public class NewsManageController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbTinTucs.AsNoTracking().OrderBy(x => x.MaTinTuc).ToList();
            PagedList<TbTinTuc> pagedListItem = new PagedList<TbTinTuc>(listItem, pageNumber, pageSize);
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
            var listItem = db.TbTinTucs.AsNoTracking().Where(x => x.TieuDe.ToLower().Contains(search)).OrderBy(x => x.MaTinTuc).ToList();
            PagedList<TbTinTuc> pagedListItem = new PagedList<TbTinTuc>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.NguoiDang = new SelectList(db.TbQuanTriViens.ToList(), "TenNguoiDung", "TenNguoiDung");
            return View();
        }

        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbTinTuc tinTuc)
        {
            db.TbTinTucs.Add(tinTuc);
            db.SaveChanges();
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "NewsManage");
        }

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            var tinTuc = db.TbTinTucs.SingleOrDefault(x => x.MaTinTuc == id);
            ViewBag.name = name;
            return View(tinTuc);
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            ViewBag.NguoiDang = new SelectList(db.TbQuanTriViens.ToList(), "TenNguoiDung", "TenNguoiDung");
            var tinTuc = db.TbTinTucs.Find(id);
            ViewBag.name = name;
            return View(tinTuc);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbTinTuc tinTuc)
        {
            db.Entry(tinTuc).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Sửa thành công";
            return RedirectToAction("Index", "NewsManage");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["Message"] = "";
            db.Remove(db.TbTinTucs.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index", "NewsManage");
        }
    }
}
