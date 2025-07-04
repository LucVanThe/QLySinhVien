using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace QLySinhVien.Areas.Student.Models
{
    public class StudenAuthentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("Email") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                     { "area", "Student" },
                    {"controller", "Account"},
                    {"action", "Login"}
                });
            }
        }
    }
}
