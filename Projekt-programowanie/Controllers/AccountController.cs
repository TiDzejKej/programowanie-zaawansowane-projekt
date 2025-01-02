using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Models;

namespace ProjektProgramowanie.Controllers
{
	[Authorize(Roles = "admin")]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		// GET: Account/Index
		public async Task<IActionResult> Index()
		{
			var users = await _userManager.Users.ToListAsync();
			var usersWithRoles = new List<UserWithRolesViewModel>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				usersWithRoles.Add(new UserWithRolesViewModel
				{
					UserId = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					Adress = user.Adress,
					CreatedAt = user.CreatedAt,
					Roles = roles
				});
			}

			return View(usersWithRoles);
		}


		// GET: /Account/Register
		[HttpGet]
		public IActionResult Register()
		{
			var roles = _roleManager.Roles.Select(r => r.Name).ToList();
			var model = new RegisterViewModel
			{
				AvailableRoles = roles
			};

			return View(model);
		}


		// POST: /Account/Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            model.AvailableRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            if (ModelState.IsValid)
			{
                var user = new ApplicationUser
				{
					UserName = model.Email,
					Email = model.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Adress = model.Adress,
					CreatedAt = DateTime.UtcNow,
                    EmailConfirmed = true
                };

				var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.Role))
					{
						var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
						if (!roleResult.Succeeded)
						{
							foreach (var error in roleResult.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
							return View(model);
						}
					}
					TempData["SuccessMessage"] = $"Account {user.Email} has been created successfully!";

					return View(model);
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

        // GET: Account/Login
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }


				TempData["FailMessage"] = "The user name or password is incorrect";
                return RedirectToAction(nameof(Login)); 
            }

            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Detail/{id}
        public async Task<IActionResult> Detail(string id)
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

			var roles = await _userManager.GetRolesAsync(user);

			var model = new UserWithRolesViewModel
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Adress = user.Adress,
				CreatedAt = user.CreatedAt,
				Roles = roles
			};

			return View(model);
		}

        // GET: Account/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            var model = new UserWithRolesViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Adress = user.Adress,
                CreatedAt = user.CreatedAt
            };

            return View(model);
        }

        // POST: Account/DeleteConfirmed/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User has been successfully deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the user.";
            }
            return RedirectToAction("Index");
        }
    }
}

