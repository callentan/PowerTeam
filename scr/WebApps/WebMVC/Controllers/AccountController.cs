using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PowerTeam.WebApps.WebMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [Authorize]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var user = User as ClaimsPrincipal;

            var token = await HttpContext.GetTokenAsync("access_token");

            if (token != null)
            {
                ViewData["access_token"] = token;
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var homeUrl = Url.Action(nameof(HomeController.Index), "Home");
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = homeUrl });
        }
    }
}
