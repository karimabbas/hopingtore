using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopingStore.ViewModel;

namespace ShopingStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var userx = await _userManager.FindByEmailAsync(registerVM.Email);
                if (userx != null)
                {
                    TempData["Error"] = "This Email Address is Already Registerd Before";
                    return View("Login");
                }

                var user = new IdentityUser
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email
                };
                var result = await _userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {
                    // await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var Password = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (Password)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "admin"))
                        {
                            return RedirectToAction("Index", "Admin/Home");

                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");

                        }
                    }
                    else
                    {
                        TempData["Error"] = "Checking non Correcct , Try again";
                        return View(loginVM);
                    }
                }
                else
                {
                    TempData["Error"] = "Wrong Password, Please Try Again";

                }
            }
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}