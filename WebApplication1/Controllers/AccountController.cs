using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        public AccountController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        // Logout action to sign out the user (or author)
        public async Task<IActionResult> Logout()
        {
            // Sign out the current user (whether User or Author)
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
