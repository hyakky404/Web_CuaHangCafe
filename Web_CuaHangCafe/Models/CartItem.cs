namespace Web_CuaHangCafe.Models
{
    public class CartItem
    {
        public string? MaSp { get; set; }
        public string? TenSp { get; set; }
        public string? AnhSp { get; set; }
        public decimal? DonGia { get; set; }
        public int? SoLuong { get; set; }
        public decimal? ThanhTien => SoLuong * DonGia;
    }
}
