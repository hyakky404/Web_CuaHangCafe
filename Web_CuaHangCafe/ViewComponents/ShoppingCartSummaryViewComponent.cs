using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web_CuaHangCafe.Helpers;
using Web_CuaHangCafe.Models;

public class ShoppingCartSummaryViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cartItems = HttpContext.Session.Get<List<CartItem>>("GioHang");
        var cartItemCount = (cartItems != null) ? cartItems.Count : 0;

        return View(cartItemCount);
    }
}
