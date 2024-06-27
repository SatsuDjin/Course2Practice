using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;

namespace MVCPract2Kurs.Help
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Проверяем, авторизован ли пользователь
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            var dbContext = context.HttpContext.RequestServices.GetService<ClinicContext>();
            var userEmail = context.HttpContext.User.Identity.Name;

            var user = dbContext.Users.SingleOrDefault(u => u.Email == userEmail);
            if (user == null || !user.IsAdmin)
            {
                // Если пользователь не администратор
                context.Result = new RedirectToActionResult("AccessDenied", "Main", null);
            }
        }
    }
}
