using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login(string returnUrl = "/Dashboard")
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl }, OpenIdConnectDefaults.AuthenticationScheme);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Callback()
    {
        return RedirectToAction("Index", "Home");
    }

}