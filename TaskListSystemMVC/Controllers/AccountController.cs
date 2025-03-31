using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskListSystemMVC.Database;
using TaskListSystemMVC.Database.Interface;
using TaskListSystemMVC.Helper;
using TaskListSystemMVC.Models;

namespace TaskListSystemMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMasterHelper helper;
        private readonly IAccountHelper accHelper;

        public AccountController(IMasterHelper masterHelper, IAccountHelper accountHelper)
        {
            helper = masterHelper;
            accHelper = accountHelper;
        }

        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userList = await helper.GetAccountInfoAll();
            
            var user = userList.Where(x => x.Email == model.Email).FirstOrDefault();

            if (user == null || !accHelper.VerifyPassword(model.Password, user.Password))
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View("~/Views/Account/Login.cshtml", model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.WindowsAccountName, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
