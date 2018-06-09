using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookieAuthentication.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CookieAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel == null &&
                string.IsNullOrWhiteSpace(loginViewModel.Username) &&
                string.IsNullOrWhiteSpace(loginViewModel.Password)
                )
            {
                return RedirectToAction(nameof(Index));
            }
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,loginViewModel.Username)
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
