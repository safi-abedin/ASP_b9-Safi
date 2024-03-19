using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using StackOverFlow.Infrastructure.Membership;
using Autofac;
using StackOverFlow.Web.Models;

namespace StackOverFlow.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly ILifetimeScope _scope;  

        private readonly ILogger<AccountController> _logger;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
                                , RoleManager<ApplicationRole> roleManager, ILifetimeScope scope,ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _scope = scope;
            _logger = logger;
        }

        
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var model = _scope.Resolve<RegistrationModel>();

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");

 
            if (ModelState.IsValid)
            {
                var user =   new ApplicationUser { UserName = model.FirstName + " " + model.LastName ,Email = model.Email 
                                                   ,FirstName = model.FirstName ,LastName = model.LastName };

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = userId, code = code, returnUrl =model.ReturnUrl },
                        protocol: Request.Scheme);

                   _logger.LogInformation($"{callbackUrl}");

                    /*await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*/

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(model.ReturnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }



    }
}
