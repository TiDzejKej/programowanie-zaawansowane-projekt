using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_programowanie.Models;
using ProjektProgramowanie.Models;

namespace Projekt_programowanie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProjektProgramowanie.Models.UserRole> UserRole { get; set; } = default!;
        public DbSet<ProjektProgramowanie.Models.User> User { get; set; } = default!;
    }
}
