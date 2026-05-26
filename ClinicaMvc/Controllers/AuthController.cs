using Microsoft.AspNetCore.Mvc;

namespace ClinicaMVC.Controllers;

public class AuthController(IConfiguration configuration) : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetString("AdminAutenticado") == "true")
        {
            return RedirectToAction("Index", "AdminMedicamentos");
        }

        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string usuario, string password, string? returnUrl = null)
    {
        var adminUser = configuration["AdminUser:Username"] ?? "admin";
        var adminPassword = configuration["AdminUser:Password"] ?? "admin123";

        if (usuario == adminUser && password == adminPassword)
        {
            HttpContext.Session.SetString("AdminAutenticado", "true");

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "AdminMedicamentos");
        }

        ViewBag.ReturnUrl = returnUrl;
        ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
