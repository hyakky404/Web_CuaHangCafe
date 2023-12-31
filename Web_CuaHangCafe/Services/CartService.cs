using Web_CuaHangCafe.Helpers;
using Web_CuaHangCafe.ViewModels;

namespace Web_CuaHangCafe.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(CartItem2 item)
        {
            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(i => i.ProductId == productId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SaveCart(cart);
            }
        }

        public List<CartItem2> GetCartItems()
        {
            return GetCart();
        }

        public decimal GetCartTotal()
        {
            var cart = GetCart();
            return (decimal)cart.Sum(item => item.Price * item.Quantity);
        }

        private List<CartItem2> GetCart()
        {
            var sessionCart = _httpContextAccessor.HttpContext.Session.Get<List<CartItem2>>("Cart");
            return sessionCart ?? new List<CartItem2>();
        }

        private void SaveCart(List<CartItem2> cart)
        {
            _httpContextAccessor.HttpContext.Session.Set("Cart", cart);
        }
    }
}
