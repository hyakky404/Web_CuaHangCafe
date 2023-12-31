using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Clients")]
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
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
            var listItem = _context.TbKhachHangs.AsNoTracking().OrderBy(x => x.SdtkhachHang).ToList();
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

            var listItem = _context.TbKhachHangs.AsNoTracking().Where(x => x.TenKhachHang.ToLower().Contains(search)).OrderBy(x => x.SdtkhachHang).ToList();
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
            _context.TbKhachHangs.Add(khachHang);
            _context.SaveChanges();

            TempData["Message"] = "Thêm thành công";

            return RedirectToAction("Index", "Clients");
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            var khachHang = _context.TbKhachHangs.Find(id);
            ViewBag.name = name;

            return View(khachHang);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbKhachHang khachHang)
        {
            _context.Entry(khachHang).State = EntityState.Modified;
            _context.SaveChanges();

            TempData["Message"] = "Sửa thành công";

            return RedirectToAction("Index", "Clients");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";

            var hoaDon = _context.TbHoaDonBans.Where(x => x.MaHoaDon == Guid.Parse(id)).ToList();

            if (hoaDon.Count() > 0)
            {
                TempData["Message"] = "Xoá không thành công";
                return RedirectToAction("Index", "Clients");
            }

            _context.Remove(_context.TbKhachHangs.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Xoá thành công";

            return RedirectToAction("Index", "Clients");
        }
    }
}
