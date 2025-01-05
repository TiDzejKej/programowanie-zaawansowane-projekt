﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class LoginViewModel
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
		[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }  
	}
}
