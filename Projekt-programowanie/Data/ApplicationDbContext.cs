using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Models;

namespace ProjektProgramowanie.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			var admin = new IdentityRole("admin");
			admin.NormalizedName = "ADMIN";

			var student = new IdentityRole("student");
			student.NormalizedName = "STUDENT";

			var lecturer = new IdentityRole("lecturer");
			lecturer.NormalizedName = "LECTURER";

			var employee = new IdentityRole("employee");
			employee.NormalizedName = "EMPLOYEE";

			builder.Entity<IdentityRole>().HasData(admin, student, lecturer, employee);
		}
	}
}
