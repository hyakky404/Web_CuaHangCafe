using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Bill")]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillController(ApplicationDbContext context)
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
            var listItem = _context.TbHoaDonBans.AsNoTracking().OrderByDescending(x => x.NgayBan).ToList();
            PagedList<TbHoaDonBan> pagedListItem = new PagedList<TbHoaDonBan>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Search")]
        [Authentication]
        [HttpGet]
        public IActionResult Search(int? page, string search)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            ViewBag.search = search;

            var listItem = _context.TbHoaDonBans.AsNoTracking().Where(x => x.NgayBan.Value.ToString().Contains(search)).OrderBy(x => x.MaHoaDon).ToList();
            PagedList<TbHoaDonBan> pagedListItem = new PagedList<TbHoaDonBan>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(int? page, string id, string name)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbChiTietHoaDonBans.AsNoTracking().Where(x => x.MaHoaDon == Guid.Parse(id)).OrderBy(x => x.MaHoaDon).ToList();
            PagedList<TbChiTietHoaDonBan> pagedListItem = new PagedList<TbChiTietHoaDonBan>(listItem, pageNumber, pageSize);

            ViewBag.name = name;

            return View(pagedListItem);
        }
    }
}
