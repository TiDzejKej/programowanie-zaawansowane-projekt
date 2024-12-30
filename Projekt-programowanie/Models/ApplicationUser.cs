using Microsoft.AspNetCore.Identity;

namespace ProjektProgramowanie.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public string? Adress { get; set; }
		public DateTime CreatedAt { get; set; }

        public ICollection<Group> GroupsLed { get; set; } = new List<Group>();

        public ICollection<Group> GroupsJoined { get; set; } = new List<Group>();

    }
}
