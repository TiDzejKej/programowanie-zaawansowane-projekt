using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
    public class UserWithRolesViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }

        [Required]
        public IEnumerable<string> UserRoles { get; set; }
    }
}
