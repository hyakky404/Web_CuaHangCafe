using Web_CuaHangCafe.Data;
using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Repository
{
    public class NhomSpRepository : INhomSpRepository
    {
        private readonly ApplicationDbContext _context;

        public NhomSpRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TbNhomSanPham Add(TbNhomSanPham nhomSanPham)
        {
            _context.TbNhomSanPhams.Add(nhomSanPham);
            _context.SaveChanges();
            return nhomSanPham;
        }

        public TbNhomSanPham Delete(string maNhomSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TbNhomSanPham> GetAllNhomSp()
        {
            return _context.TbNhomSanPhams;
        }

        public TbNhomSanPham GetAllNhomSp(string maNhomSp)
        {
            return _context.TbNhomSanPhams.Find(maNhomSp);
        }

        public TbNhomSanPham Update(TbNhomSanPham nhomSp)
        {
            _context.Update(nhomSp);
            _context.SaveChanges();
            return nhomSp;
        }
    }
}
