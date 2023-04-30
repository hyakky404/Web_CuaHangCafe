using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feedback")]
    public class FeedbackController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [Route("")]
        [Route("Index")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listItem = db.TbPhanHois.AsNoTracking().OrderBy(x => x.MaPhanHoi).ToList();
            PagedList<TbPhanHoi> pagedListItem = new PagedList<TbPhanHoi>(listItem, pageNumber, pageSize);
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
            var listItem = db.TbPhanHois.AsNoTracking().Where(x => x.TieuDe.ToLower().Contains(search)).OrderBy(x => x.MaPhanHoi).ToList();
            PagedList<TbPhanHoi> pagedListItem = new PagedList<TbPhanHoi>(listItem, pageNumber, pageSize);
            return View(pagedListItem);
        }

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(string id, string name)
        {
            var phanHoi = db.TbPhanHois.SingleOrDefault(x => x.MaPhanHoi == id);
            ViewBag.name = name;
            return View(phanHoi);
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";
            db.Remove(db.TbPhanHois.Find(id));
            db.SaveChanges();
            TempData["Message"] = "Xoá thành công";
            return RedirectToAction("Index", "Feedback");
        }
    }
}
