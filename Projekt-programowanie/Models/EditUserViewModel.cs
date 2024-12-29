using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class EditUserViewModel
	{
		public string Id { get; set; }

		[Required]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Adress")]
		public string Adress { get; set; }

		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Password")]
		public string Password { get; set; }
	}
}