using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLoginRegister.Models;
using WebApplicationLoginRegister.ViewModels;

namespace WebApplicationLoginRegister.Controllers
{
    public class UserController(UserManager<AppUser> _userManager) : Controller
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
    }
}
