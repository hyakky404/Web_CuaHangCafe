using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_CuaHangCafe.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("TenNguoiDung") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "access" },
                    {"action", "login" }
                });
            }
        }
    }
}
