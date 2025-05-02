using Demo.DataAccess.model.IdentityModel;
using Demo.Presentation.Models;
using Demo.presntation.Utilities;
using Demo.presntation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Common;

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


        #region  ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword() => View();


        [HttpPost]

        public IActionResult SentResetPasswordLink(ForgetPassword forgetPassword)
        {
            if(ModelState.IsValid) 
            {
                var user = _userManager.FindByEmailAsync(forgetPassword.Email).Result;

                if (user is not null)
                {
                    var Token=_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { forgetPassword.Email ,Token }, Request.Scheme);
                    var email = new Email()
                    {
                        To=forgetPassword.Email,
                        Subject="Reset Password",
                        Body= ResetPasswordLink

                    };

                     EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }



            }
            ModelState.AddModelError(string.Empty, "Invaild Operation");
            return View(nameof(ForgetPassword),forgetPassword);

           
           
        }

        public IActionResult CheckYourInbox() => View();



        [HttpGet]
        public IActionResult ResetPassword(string email ,string Token)
        {
            TempData["Token"] = Token;
            TempData["Email"] = email;

            return View();
        }


        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if(!ModelState.IsValid) return View(resetPasswordViewModel);

            string email = TempData["Email"] as string ?? string.Empty;
            string token=TempData["token"]as string?? string.Empty;

            var User=_userManager.FindByEmailAsync(email).Result;

            if(User is not null)
            {
               var Result=_userManager.ResetPasswordAsync(User, token,resetPasswordViewModel.PassWord).Result;

                if(Result.Succeeded)
                {
                    return RedirectToAction(nameof(login));

                }
                else
                {
                    foreach(var item in Result.Errors)
                    {

                        ModelState.AddModelError(string.Empty, item.Description);

                    }
                }

            }

            return View(nameof(ResetPassword),resetPasswordViewModel);

        }
        #endregion

    }
}
