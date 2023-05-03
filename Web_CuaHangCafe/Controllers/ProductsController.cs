using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using X.PagedList;
using Web_CuaHangCafe.Helpers;
using System.Drawing.Printing;

namespace Web_CuaHangCafe.Controllers
{
    public class ProductsController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public IActionResult Index(int? page)
        {
            int pageSize = 18;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbSanPhams.AsNoTracking().OrderBy(x => x.MaSanPham).ToList();
            PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        public IActionResult ProductType(string target, int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbSanPhams.AsNoTracking().Where(x => x.MaNhomSp == target).OrderBy(x => x.TenSanPham).ToList();
            PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);
            ViewBag.target = target;
            return View(pagedListItem);
        }

        public IActionResult Details(string id)
        {
            var products = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == id);
            return View(products);
        }
    }
}
