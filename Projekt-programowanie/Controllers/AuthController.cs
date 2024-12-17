using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projekt_programowanie.Enums;
using Projekt_programowanie.Models;
using Projekt_programowanie.ViewModels;
using System.Diagnostics;

namespace Projekt_programowanie.Controllers
{
    public class AuthController : Controller
    {

		private readonly UserManager<UserModel> _userManager;

		public AuthController(UserManager<UserModel> userManager)
		{
			_userManager = userManager;
		}


		public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
			return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM model)
		{

			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			UserModel? existingUser = await _userManager.FindByEmailAsync(model.Email);
			
			if (existingUser != null)
			{
				ModelState.AddModelError("Email", "This email is already in use");
				
			}
			return View(model);
			UserModel user = new UserModel
			{
				UserName = model.Username,
				Email = model.Email,
				Role = model.Role ?? (int)UserRole.Student 
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				TempData["SuccessMessage"] = "Registration successful!";
				return RedirectToAction("Login");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View();
		}



		public IActionResult Logout()
        {
            return View();
        }
    }
}
