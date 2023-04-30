using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Dynamic;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.MergeData;

namespace Web_CuaHangCafe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MergeModel value = new MergeModel();
            var lstProducts = db.TbSanPhams.AsNoTracking().OrderBy(x => x.MaSanPham).Take(8).ToList();
            var lstNews = db.TbTinTucs.AsNoTracking().OrderByDescending(x => x.NgayDang).Take(3).ToList();
            value.lstSanPham = lstProducts;
            value.lstTinTuc = lstNews;
            return View(value);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}