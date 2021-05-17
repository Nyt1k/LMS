using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMS.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost][HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> IsEmailInUse(string email)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return Json(true);
			}
			else
			{
				return Json($"Email {email} is alreary in use.");
			}
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
					{
						return RedirectToAction("ListUsers", "Administration");
					}

					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("index", "home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Login(string returnUrl)
		{
			LoginViewModel model = new LoginViewModel
			{
				ReturnUrl = returnUrl,
				ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
			};

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
		{
			model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(returnUrl) &&	Url.IsLocalUrl(returnUrl))
					{
						return LocalRedirect(returnUrl);
					}
					else
					{
						return RedirectToAction("index", "home");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt");

				}
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("index", "home");
		}


		[AllowAnonymous]
		public IActionResult ExternalLogin(string provider, string returnUrl)
		{
			var redirectUrl = Url.Action("ExternalLoginCallBack", "Account", new { ReturnUrl = returnUrl });

			var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
			
			return new ChallengeResult(provider, properties);
		}


		[AllowAnonymous]
		public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remoteError = null)
		{
			returnUrl ??= Url.Content("~/");

			LoginViewModel loginViewModel = new LoginViewModel
			{
				ReturnUrl = returnUrl,
				ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
			};

			if(remoteError != null)
			{
				ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
				return View("Login", loginViewModel);
			}

			var info = await signInManager.GetExternalLoginInfoAsync();
			if(info == null)
			{
				ModelState.AddModelError(string.Empty, $"Error loading external login information.");
				return View("Login", loginViewModel);
			}

			var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
				info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

			if (signInResult.Succeeded)
			{
				return LocalRedirect(returnUrl);
			}
			else
			{
				var email = info.Principal.FindFirstValue(ClaimTypes.Email);

				if(email != null)
				{
					var user = await userManager.FindByEmailAsync(email);

					if(user == null)
					{
						user = new ApplicationUser
						{
							UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
							Email = info.Principal.FindFirstValue(ClaimTypes.Email)
						};

						await userManager.CreateAsync(user);
					}

					await userManager.AddLoginAsync(user, info);
					await signInManager.SignInAsync(user, isPersistent: false);

					return LocalRedirect(returnUrl);
				}

				ViewBag.ErrorTitle = $"Email claim not recived from: {info.LoginProvider}";
				ViewBag.ErrorMessage = "Please contact support on Rofl1337@gmail.com";

				return View("Error");
			}

		}
	}
}
