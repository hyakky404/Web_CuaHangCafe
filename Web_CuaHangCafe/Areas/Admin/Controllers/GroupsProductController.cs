using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/GroupsProduct")]
    public class GroupsProductController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbNhomSanPhams.AsNoTracking().OrderBy(x => x.MaNhomSp).ToList();
            PagedList<TbNhomSanPham> pagedListItem = new PagedList<TbNhomSanPham>(listItem, pageNumber, pageSize);
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
            var listItem = db.TbNhomSanPhams.AsNoTracking().Where(x => x.TenNhomSp.ToLower().Contains(search)).OrderBy(x => x.MaNhomSp).ToList();
            PagedList<TbNhomSanPham> pagedListItem = new PagedList<TbNhomSanPham>(listItem, pageNumber, pageSize);
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
        public IActionResult Create(TbNhomSanPham nhomSp)
        {
            db.TbNhomSanPhams.Add(nhomSp);
            db.SaveChanges();
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "GroupsProduct");
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(string id, string name)
        {
            var nhomSp = db.TbNhomSanPhams.Find(id);
            ViewBag.name = name;
            return View(nhomSp);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbNhomSanPham nhomSp)
        {
            db.Entry(nhomSp).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Sửa thành công";
            return RedirectToAction("Index", "GroupsProduct");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";
            var sanPham = db.TbSanPhams.Where(x => x.MaNhomSp == id).ToList();

            if (sanPham.Count() > 0)
            {
                TempData["Message"] = "Xoá không thành công";
                return RedirectToAction("Index", "GroupsProduct");
            }

            db.Remove(db.TbNhomSanPhams.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index", "GroupsProduct");
        }
    }
}
