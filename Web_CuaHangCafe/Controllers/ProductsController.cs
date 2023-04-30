using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using X.PagedList;
using Web_CuaHangCafe.Helpers;

namespace Web_CuaHangCafe.Controllers
{
    public class ProductsController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public IActionResult Index(int? page, string target, string targetname)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            if (target == "all" || target == "")
            {
                var listItem = db.TbSanPhams.AsNoTracking().OrderBy(x => x.MaSanPham).ToList();
                PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
                ViewBag.target = target;
                ViewBag.targetname = targetname;
                return View(pagedListItem);
            }
            else
            {
                var listItem = db.TbSanPhams.AsNoTracking().Where(x => x.MaNhomSp == target).OrderBy(x => x.TenSanPham).ToList();
                PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
                ViewBag.target = target;
                ViewBag.targetname = targetname;
                return View(pagedListItem);
            }
        }

        public IActionResult Details(string maSanPham)
        {
            var sanPham = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == maSanPham);
            return View(sanPham);
        }
    }
}
