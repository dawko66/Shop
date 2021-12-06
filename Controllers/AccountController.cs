
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VODUser.Models.Identity;

namespace VODUser.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
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
        public async Task<IActionResult> Register(RegisterViewModel registermodel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registermodel.Email,
                    Email = registermodel.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, registermodel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var err in result.Errors) { ModelState.AddModelError(string.Empty, err.Description); }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new  User
                {
                    Email = loginViewModel.Email,
                    UserName = loginViewModel.Email
                };


                Microsoft.AspNetCore.Identity.SignInResult identityResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Niepoprawny login lub hasło.");
                }
            }
            return View();
        }


        [HttpGet]
        [Authorize]
        public IActionResult CheckLogin()
        {
            return Ok();
        }
    }
}
