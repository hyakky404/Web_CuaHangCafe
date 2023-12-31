using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        QlcuaHangCafeContext db = new QlcuaHangCafeContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.TbSanPhams);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var hangHoa = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == id);

                if (hangHoa == null)
                {
                    return NotFound(404);
                }

                return Ok(new
                {
                    success = true,
                    data = hangHoa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Create(TbSanPham sanPham)
        {
            Random random = new Random();

            var hangHoa = new TbSanPham
            {
                MaSanPham= random.Next(),
                TenSanPham = sanPham.TenSanPham,
                GiaBan = sanPham.GiaBan,
                MoTa = sanPham.MoTa,
                HinhAnh = sanPham.HinhAnh,
                GhiChu = sanPham.GhiChu,
                MaNhomSp = sanPham.MaNhomSp
            };

            db.TbSanPhams.Add(hangHoa);

            return Ok(new
            {
                Success = true,
                Data = hangHoa
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, TbSanPham editHangHoa)
        {
            try
            {
                var hangHoa = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == id);

                if (hangHoa == null)
                {
                    return NotFound(404);
                }

                if (id != hangHoa.MaSanPham)
                {
                    return BadRequest();
                }

                hangHoa.TenSanPham = editHangHoa.TenSanPham;
                hangHoa.GiaBan = editHangHoa.GiaBan;
                hangHoa.MoTa = editHangHoa.MoTa;
                hangHoa.HinhAnh = editHangHoa.HinhAnh;
                hangHoa.GhiChu = editHangHoa.GhiChu;
                hangHoa.MaNhomSp = editHangHoa.MaNhomSp;

                return Ok(new
                {
                    success = true,
                    data = hangHoa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var hangHoa = db.TbSanPhams.SingleOrDefault(x => x.MaSanPham == id);

                if (hangHoa == null)
                {
                    return NotFound(404);
                }

                db.TbSanPhams.Remove(hangHoa);

                return Ok(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
