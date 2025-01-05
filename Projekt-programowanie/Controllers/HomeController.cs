using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektProgramowanie.Models;
using System.Diagnostics;

namespace ProjektProgramowanie.Controllers
{
	[Authorize]
	public class HomeController : Controller

	{
		private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        // using UserListViewModel from Models folder to display count of users and roles
        public async Task<IActionResult> Users()
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

    }
}
