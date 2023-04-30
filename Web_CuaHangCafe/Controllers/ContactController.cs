using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Controllers
{
    public class ContactController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

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
                    db.TbPhanHois.Add(phanHoi);
                    db.SaveChanges();
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
