using Projekt_programowanie.Enums;
using System.ComponentModel.DataAnnotations;

namespace Projekt_programowanie.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username required")]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password required")]
        [Compare("Password", ErrorMessage = "Password must match")]
        public string? ConfirmPassword { get; set; }

        public int? Role {  get; set; }

        public List<UserRoleItem> userRoleItems { get; set; }
    }
}
