using Microsoft.AspNetCore.Mvc;
using Projekt_programowanie.Enums;
using Projekt_programowanie.ViewModels;

namespace Projekt_programowanie.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            var vm = new RegisterVM();
            vm.userRoleItems = UserRoleGetter.GetList();
			return View(vm);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
