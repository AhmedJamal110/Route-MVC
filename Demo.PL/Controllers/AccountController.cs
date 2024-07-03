using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsAgree = model.IsAgree
                };

                var Result = await _userManager.CreateAsync(user, model.Password);
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var item in Result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
            }

            return View(model);

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemmberMe, false);

                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Password");
                }

                ModelState.AddModelError(string.Empty, "Invlaid Email");

            }


            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }



        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                   var resetPasswordLink = Url.Action("ResetPassword", "Account",
                        new { email = user.Email, Token = token }, "https", "localhost:44348");



                    var email = new Email()
                    {
                        Body = resetPasswordLink,
                        Subject = "Reset Password",
                        To = model.Email
                    };

                    EmailSetting.SendEmail(email);

                    return RedirectToAction(nameof(ChechYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Eamil not found ");
            }

            return View(model);

        }

        public IActionResult ChechYourInbox()
        {
            return View();
        }



        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token  = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);

               var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description); 
            
            }


            return View(model);
        }

	
         
    }
}

