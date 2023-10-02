using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _userManager.Users.ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> CreateUser()
        {
            await _userManager.CreateAsync(new AppUser() { Email = "taleh@gmail.com", UserName = "TalehCode" },"Taleh123@" );
            await _userManager.CreateAsync(new AppUser() {Email = "samir@gmail.com",    UserName = "SamirCode"}, "Samir123@" );
            await _userManager.CreateAsync(new AppUser() {Email = "orxan@gmail.com",    UserName = "OrxanCode" }, "Orxan123@" );
            await _userManager.CreateAsync(new AppUser() { Email = "kamran@gmail.com", UserName = "KamranCode" }, "Kamran123@" );

            return Json("Ok");
        }
    }
}