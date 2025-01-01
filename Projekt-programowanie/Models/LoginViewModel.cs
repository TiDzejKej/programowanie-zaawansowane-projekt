using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class LoginViewModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }  
	}

}
