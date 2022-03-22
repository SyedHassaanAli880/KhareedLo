using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KhareedLo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using KhareedLo.Auth;
using System.Security.Claims;
using KhareedLo.Models;

namespace KhareedLo.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _db;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext apb)
        {
            _signInManager = signInManager; _userManager = userManager;

            _db = apb;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Acount", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var redirectUrl = Url.Action("ExternalLoginCallback", "Acount", new { ReturnUrl = returnUrl });

            LoginViewModel LoginViewmodel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider:{remoteError}");

                return View("Login", LoginViewmodel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external information.");

                return View("Login", LoginViewmodel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email == null)
                {
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),

                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await _userManager.CreateAsync(user);
                    }

                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
            }

            return View("Login", LoginViewmodel);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginviewModel)
        {
            if (/*!ModelState.IsValid*/ false)
            {
                return View(loginviewModel);
            }

            var user = await
                _userManager.FindByNameAsync(loginviewModel.UserName);

            if (user != null)
            {
                var result = await
                    _signInManager.PasswordSignInAsync(user, loginviewModel.Password, false, false);


                if (result.Succeeded)
                {
                    UserClass.name = loginviewModel.UserName;

                    return RedirectToAction("Index", "Product");
                }
            }

            ModelState.AddModelError("", "Username/password not found!");

            return View(loginviewModel);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel loginviewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                { UserName = loginviewModel.UserName,
                    Email = loginviewModel.Email
                };

                var result = await _userManager.CreateAsync(user, loginviewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

            }

            return View(loginviewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Product");
        }
    }
}
