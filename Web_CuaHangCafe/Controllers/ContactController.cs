using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web_CuaHangCafe.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TbPhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.TbPhanHois.Add(phanHoi);
                    _context.SaveChanges();
                    TempData["Status"] = "success";
                    TempData["Message"] = "Gửi thành công";
                    return RedirectToAction("index");
                }
                catch
                {
                    TempData["Status"] = "error";
                    TempData["Message"] = "Không gửi được lời nhắn";
                }
            }  

            return View(phanHoi);
        }
    }
}
