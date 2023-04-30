using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.Repository
{
    public interface INhomSpRepository
    {
        TbNhomSanPham Add(TbNhomSanPham nhomSp);
        TbNhomSanPham Update(TbNhomSanPham nhomSp);
        TbNhomSanPham Delete(String maNhomSp);
        TbNhomSanPham GetAllNhomSp(String maNhomSp);
        IEnumerable<TbNhomSanPham> GetAllNhomSp();
    }
}
