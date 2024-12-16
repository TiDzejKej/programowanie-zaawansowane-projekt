using System.ComponentModel.DataAnnotations;

namespace Projekt_programowanie.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string? Password { get; set; }
    }
}
