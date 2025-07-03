using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Web_CuaHangCafe.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = _context.TbTinTucs.AsNoTracking().OrderByDescending(x => x.NgayDang).ToList();
            PagedList<TbTinTuc> pagedListItem = new PagedList<TbTinTuc>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        public IActionResult Details(int? maTinTuc)
        {
            var tinTuc = _context.TbTinTucs.SingleOrDefault(x => x.MaTinTuc == maTinTuc);

            return View(tinTuc);
        }
    }
}
