using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektProgramowanie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

			var viewModel = new UserListViewModel
			{
				Users = users,
				UserRoles = userRoles
			};

			return View(viewModel);
		}
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}
	}
}
