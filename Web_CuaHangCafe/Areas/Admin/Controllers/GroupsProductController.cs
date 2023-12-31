using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/GroupsProduct")]
    public class GroupsProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbNhomSanPhams.AsNoTracking().OrderBy(x => x.MaNhomSp).ToList();
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

            var listItem = _context.TbNhomSanPhams.AsNoTracking().Where(x => x.TenNhomSp.ToLower().Contains(search)).OrderBy(x => x.MaNhomSp).ToList();
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
            _context.TbNhomSanPhams.Add(nhomSp);
            _context.SaveChanges();

            TempData["Message"] = "Thêm thành công";

            return RedirectToAction("Index", "GroupsProduct");
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            var nhomSp = _context.TbNhomSanPhams.Find(id);
            ViewBag.name = name;

            return View(nhomSp);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbNhomSanPham nhomSp)
        {
            _context.Entry(nhomSp).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Message"] = "Sửa thành công";

            return RedirectToAction("Index", "GroupsProduct");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["Message"] = "";

            var sanPham = _context.TbSanPhams.Where(x => x.MaNhomSp == id).ToList();

            if (sanPham.Count() > 0)
            {
                TempData["Message"] = "Xoá không thành công";
                return RedirectToAction("Index", "GroupsProduct");
            }

            _context.Remove(_context.TbNhomSanPhams.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Xoá thành công";

            return RedirectToAction("Index", "GroupsProduct");
        }
    }
}
