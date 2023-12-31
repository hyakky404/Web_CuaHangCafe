namespace Web_CuaHangCafe.ViewModels
{
    public class OrderViewModel
    {
        public Guid MaHoaDon { get; set; }

        public string SoHoaDon { get; set; } = null!;

        public DateTime? NgayBan { get; set; }

        public decimal? TongTien { get; set; }

        public Guid CustomerId { get; set; }
    }
}
