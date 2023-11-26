using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl }, OpenIdConnectDefaults.AuthenticationScheme);
    }

    public IActionResult Logout()
    {
        return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }

    public IActionResult Callback()
    {
        return RedirectToAction("Index", "Home");
    }
}