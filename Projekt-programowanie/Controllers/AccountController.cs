using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Models;
using Syncfusion.EJ2.FileManager;
using System.Threading.Tasks;

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
					TempData["SuccessMessage"] = "Your account has been created successfully!";


					return View(model);
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

            model.AvailableRoles = _roleManager.Roles.Select(r => r.Name).ToList();
			return View(model);
		}

		

		// GET: Account/Login
		public IActionResult Login()
		{
			return View(new LoginViewModel()); // Upewnij się, że przekazujesz pusty model, aby uniknąć null
		}

		// POST: Account/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				// Jeżeli logowanie się powiedzie, użytkownik zostanie przekierowany do strony, z której chciał przejść
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					return RedirectToLocal(returnUrl);  // Przekierowanie użytkownika na poprzednią stronę lub domyślną
				}

				ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");  // Błąd logowania
			}

			return View(model);  // Jeżeli formularz jest niepoprawny, zwrócenie widoku z komunikatem o błędzie
		}

		// Przekierowanie użytkownika do odpowiedniej strony po zalogowaniu
		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");  // Przekierowanie do strony głównej
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

		public async Task<IActionResult> Edit(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			// Pobieranie dostępnych ról
			var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

			// Pobieranie ról przypisanych do użytkownika
			var userRoles = await _userManager.GetRolesAsync(user);

			var model = new UserWithRolesViewModel
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Adress = user.Adress,
				CreatedAt = user.CreatedAt,
				Roles = roles,  // Dostępne role
				UserRoles = userRoles // Role przypisane do użytkownika
			};

			return View(model);
		}

	}




}

