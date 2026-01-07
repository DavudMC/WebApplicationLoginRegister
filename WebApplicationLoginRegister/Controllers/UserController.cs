using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLoginRegister.Models;
using WebApplicationLoginRegister.ViewModels;

namespace WebApplicationLoginRegister.Controllers
{
    public class UserController(UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existUserName = await _userManager.FindByNameAsync(registerVM.UserName);
            if (existUserName != null) 
            {
                ModelState.AddModelError("UserName", "This user already exists");
                return View(registerVM);
            }
            var existEmailAddress = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (existEmailAddress != null) 
            {
                ModelState.AddModelError(nameof(registerVM.EmailAddress), "This email already exists");
            }
            AppUser newUser = new() 
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName
            };
            var result = await _userManager.CreateAsync(newUser,registerVM.Password);
            if (!result.Succeeded) 
            {
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                    return View(registerVM);
                }
            }
            return RedirectToAction(nameof(Index),"Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var existUser = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (existUser == null)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(loginVM);
            }
            var existUserPassword = await _userManager.CheckPasswordAsync(existUser, loginVM.Password);
            if (!existUserPassword)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(loginVM);
            }
            await _signInManager.SignInAsync(existUser, loginVM.IsRemember);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
