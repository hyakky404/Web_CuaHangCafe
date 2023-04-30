using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using X.PagedList;

namespace Web_CuaHangCafe.Controllers
{
    public class NewsController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbTinTucs.AsNoTracking().OrderByDescending(x => x.NgayDang).ToList();
            PagedList<TbTinTuc> pagedListItem = new PagedList<TbTinTuc>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        public IActionResult Details(string? maTinTuc)
        {
            var tinTuc = db.TbTinTucs.SingleOrDefault(x => x.MaTinTuc == maTinTuc);
            return View(tinTuc);
        }
    }
}
