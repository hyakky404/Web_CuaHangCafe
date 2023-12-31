using Web_CuaHangCafe.ViewModels;

namespace Web_CuaHangCafe.Services
{
    public interface ICartService
    {
        void AddToCart(CartItem2 item);
        void RemoveFromCart(int productId);
        List<CartItem2> GetCartItems();
        decimal GetCartTotal();
    }
}
