using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using X.PagedList;

namespace Web_CuaHangCafe.Controllers
{
    public class SearchController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public IActionResult Index(int? page, string search)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            search = search.ToLower();
            ViewBag.search = search;
            var listItem = db.TbSanPhams.AsNoTracking().Where(x => x.TenSanPham.ToLower().Contains(search)).OrderBy(x => x.TenSanPham).ToList();
            PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }
    }
}
