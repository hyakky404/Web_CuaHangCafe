namespace Web_CuaHangCafe.ViewModels
{
    public class CartItem2
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductImage { get; set; } = null!;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal? ThanhTien => Quantity * Price;
    }
}
