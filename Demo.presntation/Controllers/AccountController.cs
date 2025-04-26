using Demo.DataAccess.model.IdentityModel;
using Demo.Presentation.Models;
using Demo.presntation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presntation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager ,
                                    SignInManager<ApplicationUser> signInManager) : Controller
    {

        #region  Register

        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            var user = new ApplicationUser
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                // للتأكد فقط
                Console.WriteLine("User Created!");
                return RedirectToAction("Login");
            }

            // دي أهم حاجة نضيفها
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                Console.WriteLine($"Error: {error.Description}"); // تطبع في Output
            }

            return View(registerViewModel);
        }


        #endregion


        #region  Login 

        [HttpGet]

        public IActionResult login() => View();

        [HttpPost]

        public IActionResult login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;

            if (user is not null)
            {
                bool flag = _userManager.CheckPasswordAsync (user, loginViewModel.Password).Result;
                if (flag)
                {
                    var Result = signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "the user is Not Allowed ");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "the user is locked out ");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                }
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Invalid ");
            }


            return View(loginViewModel);


        }

        #endregion
    }
}
