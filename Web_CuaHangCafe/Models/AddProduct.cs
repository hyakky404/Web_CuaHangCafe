namespace Web_CuaHangCafe.Models
{
    public class AddProduct
    {
        public int MaSanPham { get; set; }

        public string TenSanPham { get; set; } = null!;

        public int MaNhomSp { get; set; }

        public decimal? GiaBan { get; set; }

        public string? MoTa { get; set; }

        public string? HinhAnh { get; set; }

        public string? GhiChu { get; set; }

    }
}
