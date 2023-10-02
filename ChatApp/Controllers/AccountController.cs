using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {
            var user = await _userManager.FindByNameAsync(appUser.UserName);
            
            var result = await _signIn.PasswordSignInAsync(user,appUser.PasswordHash,true,true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username and password wrong!");
                return View();
            }

            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
