using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwoFactorAuth.Models;
using TwoFactorAuth.SmsHelper;

namespace TwoFactorAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;


        private readonly IConfiguration _configuration;
        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

            _configuration = configuration;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(model.Email);
                if (userCheck == null)
                {
                    var user = new AppUser() { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };


                    var result = await userManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {
                        var userResult = await userManager.FindByEmailAsync(model.Email);
                        var userId = await userManager.GetUserIdAsync(userResult);
                        var code = await userManager.GenerateEmailConfirmationTokenAsync(userResult);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        return RedirectToAction("LoginTwoStep", new { email = user.Email });
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(model);
                }
            }
            return View(model);

        }

        //[HttpGet, AllowAnonymous]
        //public async Task<IActionResult> EnableTwoFactor(string email, string code)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    EnableFactor model = new EnableFactor();
        //    if (user != null)
        //    {
        //        model.Id = user.Id;
        //        model.EmailConfirmed = user.EmailConfirmed;
        //        model.TwoFactorEnabled = user.TwoFactorEnabled;
        //        model.Email = user.Email;
        //        model.code = code;
        //    }
        //    else
        //    {

        //        ModelState.AddModelError("message", "Email not Found");
        //    }

        //    return View(model);
        //}

        //[HttpPost, AllowAnonymous]
        //public async Task<IActionResult> EnableTwoFactor(EnableFactor model)
        //{

        //    var userCheck = await userManager.FindByIdAsync(model.Id);
        //    if (userCheck != null)
        //    {

        //        if (model.Id == null || model.code == null)
        //        {
        //            return View("Login");
        //        }
        //        model.code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.code));
        //        var result = await userManager.ConfirmEmailAsync(userCheck, model.code);


        //        if (!result.Succeeded)
        //        {
        //            return View("Login");
        //        }
        //        await userManager.SetTwoFactorEnabledAsync(userCheck, true);
        //        TwoFactor mytwoFactor = new TwoFactor()
        //        {
        //            Email = userCheck.Email
        //        };
        //        return RedirectToAction("LoginTwoStep", new { twoFactor = mytwoFactor });


        //    }
        //    else
        //    {
        //        ModelState.AddModelError("message", "Error to set Values.");
        //        return View(model);
        //    }


        //}

        public ActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email)
        {

            var user = await userManager.FindByEmailAsync(email);

            var code = await userManager.GenerateChangePhoneNumberTokenAsync(
                               user, user.PhoneNumber);

            SmsService emailHelper = new SmsService();
            emailHelper.SendSmsAsync(user.PhoneNumber, code);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor.TwoFactorCode);
            }
            var myuser = await userManager.FindByEmailAsync(twoFactor.Email);
            var result = await userManager.ChangePhoneNumberAsync(myuser, myuser.PhoneNumber, twoFactor.TwoFactorCode);
            if (result.Succeeded)
            {

                if (myuser != null)
                {
                    await signInManager.SignInAsync(myuser, isPersistent: false);
                }
                return RedirectToAction("Index", "Products");
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View();

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));

                    return RedirectToAction("LoginTwoStep", new { email = user.Email });

                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }



    }
}
