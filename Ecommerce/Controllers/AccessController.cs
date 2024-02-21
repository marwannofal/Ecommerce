using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _Context;
        public AccessController(AppDbContext context)
        {
            _Context = context;
        }
        //LogUp
        public IActionResult LogUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogUp([Bind("id,Email,Password,KeepLoggedIn")] VMLogin vMLogin, string b1)
        {
            if (b1 == "LogUp")
            {
                if (ModelState.IsValid)
                {
                    _Context.Add(vMLogin);
                    await _Context.SaveChangesAsync();
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier , vMLogin.Email),
                        new Claim("OtherProperties" , "Example Role")
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = vMLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    return RedirectToAction("Index", "Movies");
                }
            }
            return View(vMLogin);
        }
        //Login
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Movies");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VMLogin vMLogin, string b1)
        {
            if (b1 == "LogIn")
            {
                if (UserExists(vMLogin.Email))

                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier , vMLogin.Email),
                        new Claim("OtherProperties" , "Example Role")
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = vMLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    return RedirectToAction("Index", "Movies");
                }
                else
                {
                    return View("NotFound");
                }
            }
            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
        private bool UserExists(string Email)
        {
            return (_Context.VMLogins?.Any(e => e.Email == Email)).GetValueOrDefault();
        }
    }
}