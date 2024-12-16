using Microsoft.AspNetCore.Identity;
using Projekt_programowanie.Enums;

namespace Projekt_programowanie.Models
{
    
    public class UserModel : IdentityUser
    {
        public int Role { get; set; } = (int)UserRole.Student; // default student
    }
}
