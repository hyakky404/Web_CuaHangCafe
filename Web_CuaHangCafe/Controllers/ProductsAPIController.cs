using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : Controller
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [HttpGet]
        public IEnumerable<TbSanPham> GetAllProducts()
        {
            var sanPham = (from p in db.TbSanPhams
                           select new TbSanPham
                           {
                               MaSanPham = p.MaSanPham,
                               TenSanPham = p.TenSanPham,
                               MaNhomSp = p.MaNhomSp,
                               HinhAnh = p.HinhAnh,
                               GiaBan = p.GiaBan,
                           }).ToList();
            return sanPham;
        }

        [HttpGet("{maNhomSp}")]
        public IEnumerable<TbSanPham> GetProductsByCategory(int maNhomSp)
        {
            var sanPham = (from p in db.TbSanPhams
                           where p.MaNhomSp == maNhomSp
                           select new TbSanPham
                           {
                               MaSanPham = p.MaSanPham,
                               TenSanPham = p.TenSanPham,
                               MaNhomSp = p.MaNhomSp,
                               HinhAnh = p.HinhAnh,
                               GiaBan = p.GiaBan,
                           }).ToList();
            return sanPham;
        }
    }
}
