using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class ApplicationUser : IdentityUser
	{
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(200)]
        public string? Adress { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<Group> GroupsLed { get; set; } = new List<Group>();

        public ICollection<Group> GroupsJoined { get; set; } = new List<Group>();

    }
}
