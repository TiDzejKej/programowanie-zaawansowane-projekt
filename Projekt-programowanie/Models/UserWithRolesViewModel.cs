namespace ProjektProgramowanie.Models
{
	public class UserWithRolesViewModel
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Adress { get; set; }
		public DateTime CreatedAt { get; set; }
		public IEnumerable<string> Roles { get; set; }
		public IEnumerable<string> UserRoles { get; set; }
	}
}

