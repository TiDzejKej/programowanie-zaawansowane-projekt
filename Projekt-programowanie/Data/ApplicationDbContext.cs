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

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Group> Groups { get; set; }
		public DbSet<Survey> Surveys { get; set; }

        public DbSet<ContactForm> ContactForms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithMany(u => u.GroupsLed)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Group>()
                .HasMany(g => g.Students)
                .WithMany(u => u.GroupsJoined)
                .UsingEntity(j => j.ToTable("GroupStudents"));
        }
        public DbSet<SurveyViewModel> SurveyViewModel { get; set; }
    }
}
