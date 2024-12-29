using System.Collections.Generic;

namespace ProjektProgramowanie.Models
{
	public class UserListViewModel
	{
		public IEnumerable<ApplicationUser> Users { get; set; }
		public Dictionary<string, IList<string>> UserRoles { get; set; }
	}
}