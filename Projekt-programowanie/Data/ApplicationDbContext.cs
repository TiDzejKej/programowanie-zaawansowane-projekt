using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Models;
using System.Reflection.Emit;

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

			builder.Entity<Question>()
				.HasOne(q => q.Survey)
				.WithMany(s => s.Questions)
				.HasForeignKey(q => q.SurveyId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Answer>()
				.HasOne(a => a.Survey)
				.WithMany()
				.HasForeignKey(a => a.SurveyId);

			builder.Entity<Answer>()
				.HasOne(a => a.Question)
				.WithMany()
				.HasForeignKey(a => a.QuestionId);

			builder.Entity<AnswerViewModel>().HasNoKey();
		}
        public DbSet<ProjektProgramowanie.Models.Survey> Survey { get; set; } = default!;
        public DbSet<ProjektProgramowanie.Models.Answer> Answer { get; set; } = default!;
        public DbSet<ProjektProgramowanie.Models.Question> Question { get; set; } = default!;
	}
}
