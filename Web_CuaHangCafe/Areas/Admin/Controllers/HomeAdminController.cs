using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;
using Web_CuaHangCafe.Models.Authentication;
using Web_CuaHangCafe.ViewModels;
using X.PagedList;

namespace Web_CuaHangCafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("HomeAdmin")]
    public class HomeAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment hostEnvironment;

        public HomeAdminController(ApplicationDbContext context, IWebHostEnvironment hc)
        {
            _context = context;
            hostEnvironment = hc;
        }

        [Route("")]
        [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 30;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listItem = (from product in _context.TbSanPhams
                            join type in _context.TbNhomSanPhams on product.MaNhomSp equals type.MaNhomSp
                            orderby product.MaSanPham
                            select new ProductViewModel
                            {
                                MaSanPham = product.MaSanPham,
                                TenSanPham = product.TenSanPham,
                                GiaBan = product.GiaBan,
                                MoTa = product.MoTa,
                                HinhAnh = product.HinhAnh,
                                GhiChu = product.GhiChu,
                                LoaiSanPham = type.TenNhomSp
                            }).ToList();

            PagedList<ProductViewModel> pagedListItem = new PagedList<ProductViewModel>(listItem, pageNumber, pageSize);

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

            var listItem = _context.TbSanPhams.AsNoTracking().Where(x => x.TenSanPham.ToLower().Contains(search)).OrderBy(x => x.MaSanPham).ToList();
            PagedList<TbSanPham> pagedListItem = new PagedList<TbSanPham>(listItem, pageNumber, pageSize);

            return View(pagedListItem);
        }

        [Route("Create")]
        [Authentication]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaNhomSp = new SelectList(_context.TbNhomSanPhams.ToList(), "MaNhomSp", "TenNhomSp");

            return View();
        }

        [Route("Create")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbSanPham sanPham, IFormFile imageFile)
        {
            //string fileName = "";

            //if (createProduct.HinhAnh != null)
            //{
            //    string uploadFolder = Path.Combine(Path.Combine(hostEnvironment.WebRootPath, "img"), "products");
            //    fileName = createProduct.HinhAnh.FileName;
            //    string filePath = Path.Combine(uploadFolder, fileName);
            //    createProduct.HinhAnh.CopyTo(new FileStream(filePath, FileMode.Create));
            //}

            //var product = new TbSanPham
            //{
            //    MaSanPham = createProduct.MaSanPham,
            //    TenSanPham = createProduct.TenSanPham,
            //    GiaBan = createProduct.GiaBan,
            //    MoTa = createProduct.MoTa,
            //    HinhAnh = fileName,
            //    GhiChu = createProduct.GhiChu,
            //    MaNhomSp = createProduct.MaLoaiSanPham
            //};

            if (imageFile != null && imageFile.Length > 0)
            {
                // Đối với mục đích minh họa, chúng ta sẽ lưu ảnh vào thư mục Images trong wwwroot
                string uploadFolder = Path.Combine(Path.Combine(hostEnvironment.WebRootPath, "img"), "products");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Lưu đường dẫn hoặc thông tin về ảnh vào cơ sở dữ liệu nếu cần
                // Ví dụ: lưu đường dẫn filePath vào cơ sở dữ liệu
                // ...

                return RedirectToAction("Index");
            }

            //db.TbSanPhams.Add(product);
            _context.SaveChanges();
            TempData["Message"] = "Thêm sản phẩm thành công";

            return RedirectToAction("Index", "HomeAdmin");
        }

        [Route("Details")]
        [Authentication]
        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            var productItem = (from product in _context.TbSanPhams
                            join type in _context.TbNhomSanPhams on product.MaNhomSp equals type.MaNhomSp
                            where product.MaSanPham == id
                            select new ProductViewModel
                            {
                                MaSanPham = product.MaSanPham,
                                TenSanPham = product.TenSanPham,
                                GiaBan = product.GiaBan,
                                MoTa = product.MoTa,
                                HinhAnh = product.HinhAnh,
                                GhiChu = product.GhiChu,
                                LoaiSanPham = type.TenNhomSp
                            }).SingleOrDefault();

            ViewBag.name = name;

            return View(productItem);
        }

        [Route("Edit")]
        [Authentication]
        [HttpGet]
        public IActionResult Edit(int id, string name)
        {
            var sanPham = _context.TbSanPhams.Find(id);

            ViewBag.MaNhomSp = new SelectList(_context.TbNhomSanPhams.ToList(), "MaNhomSp", "TenNhomSp");
            ViewBag.name = name;

            return View(sanPham);
        }

        [Route("Edit")]
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateProductViewModel createProduct)
        {
            string fileName = "";

            if (createProduct.HinhAnh != null)
            {
                string uploadFolder = Path.Combine(Path.Combine(hostEnvironment.WebRootPath, "img"), "products");
                fileName = createProduct.HinhAnh.FileName;
                string filePath = Path.Combine(uploadFolder, fileName);
                createProduct.HinhAnh.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            var product = new TbSanPham
            {
                MaSanPham = createProduct.MaSanPham,
                TenSanPham = createProduct.TenSanPham,
                GiaBan = createProduct.GiaBan,
                MoTa = createProduct.MoTa,
                HinhAnh = fileName,
                GhiChu = createProduct.GhiChu,
                MaNhomSp = createProduct.MaLoaiSanPham
            };

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            TempData["Message"] = "Sửa sản phẩm thành công";
            return RedirectToAction("Index", "HomeAdmin");
        }

        [Route("Delete")]
        [Authentication]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TempData["Message"] = "";
            var chiTietHoaDon = _context.TbChiTietHoaDonBans.Where(x => x.MaSanPham == id).ToList();

            if (chiTietHoaDon.Count() > 0)
            {
                TempData["Message"] = "Không xoá được sản phẩm";

                return RedirectToAction("Index", "HomeAdmin");
            }

            _context.Remove(_context.TbSanPhams.Find(id));
            _context.SaveChanges();

            TempData["Message"] = "Sản phẩm đã được xoá";

            return RedirectToAction("Index", "HomeAdmin");
        }
    }
}
