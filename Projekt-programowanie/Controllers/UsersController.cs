using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ProjektProgramowanie.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ProjektProgramowanie.Controllers
{
	[Authorize(Roles = "admin, employee")]
	public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UsersController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var users = _userManager.Users.ToList();
			var userRoles = new Dictionary<string, IList<string>>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				userRoles[user.Id] = roles;
			}

			var model = new UserListViewModel
			{
				Users = users,
				UserRoles = userRoles
			};

			return View(model);
		}

		public async Task<IActionResult> Edit(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			var model = new EditUserViewModel
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Adress = user.Adress,
				PhoneNumber = user.PhoneNumber
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(model.Id);
				if (user == null)
				{
					return NotFound();
				}

				user.UserName = model.UserName;
				user.Email = model.Email;
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				user.Adress = model.Adress;
				user.PhoneNumber = model.PhoneNumber;

				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.Password))
					{
						var token = await _userManager.GeneratePasswordResetTokenAsync(user);
						var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
						if (!passwordResult.Succeeded)
						{
							foreach (var error in passwordResult.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
							return View(model);
						}
					}
					return RedirectToAction(nameof(Index));
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}
		
		[HttpPost, ActionName("Delete")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction(nameof(Index));
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(user);
		}
	}
}