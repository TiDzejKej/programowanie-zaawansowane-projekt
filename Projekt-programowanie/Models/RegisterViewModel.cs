using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Adress")]
		public string Adress { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "Role")]
		public string Role { get; set; }

		public IEnumerable<string> AvailableRoles { get; set; } = new List<string>();
	}
}
