using Web_CuaHangCafe.Models;

namespace Web_CuaHangCafe.MergeData
{
    public class Product
    {
        public string? MaSp { get; set; }
        public string? TenSp { get; set; }
        public string? AnhSp { get; set; }
        public decimal? DonGia { get; set; }
        public int? SoLuong { get; set; }
        public decimal? ThanhTien => SoLuong * DonGia;
    }
}
