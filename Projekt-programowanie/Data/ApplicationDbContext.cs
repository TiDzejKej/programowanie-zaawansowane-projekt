using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public DbSet<SurveyViewModel> SurveyViewModel { get; set; }

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

        public async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "admin", "student", "lecturer", "employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "anna.kowalska@uczelnia.pl", Email = "anna.kowalska@uczelnia.pl", FirstName = "Anna", LastName = "Kowalska", Adress = "Adres 1", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "jan.nowak@uczelnia.pl", Email = "jan.nowak@uczelnia.pl", FirstName = "Jan", LastName = "Nowak", Adress = "Adres 2", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "kamil.zielinski@uczelnia.pl", Email = "kamil.zielinski@uczelnia.pl", FirstName = "Kamil", LastName = "Zieliński", Adress = "Adres 3", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "ewa.wrobel@uczelnia.pl", Email = "ewa.wrobel@uczelnia.pl", FirstName = "Ewa", LastName = "Wróbel", Adress = "Adres 4", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "tomasz.wisniewski@uczelnia.pl", Email = "tomasz.wisniewski@uczelnia.pl", FirstName = "Tomasz", LastName = "Wiśniewski", Adress = "Adres 5", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "marta.kaczmarek@uczelnia.pl", Email = "marta.kaczmarek@uczelnia.pl", FirstName = "Marta", LastName = "Kaczmarek", Adress = "Adres 6", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "mateusz.wozniak@uczelnia.pl", Email = "mateusz.wozniak@uczelnia.pl", FirstName = "Mateusz", LastName = "Woźniak", Adress = "Adres 7", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "justyna.kozlowska@uczelnia.pl", Email = "justyna.kozlowska@uczelnia.pl", FirstName = "Justyna", LastName = "Kozłowska", Adress = "Adres 8", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "piotr.sawicki@uczelnia.pl", Email = "piotr.sawicki@uczelnia.pl", FirstName = "Piotr", LastName = "Sawicki", Adress = "Adres 9", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "agata.michalska@uczelnia.pl", Email = "agata.michalska@uczelnia.pl", FirstName = "Agata", LastName = "Michalska", Adress = "Adres 10", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "pawel.nowakowski@uczelnia.pl", Email = "pawel.nowakowski@uczelnia.pl", FirstName = "Paweł", LastName = "Nowakowski", Adress = "Adres 11", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "karolina.szymanska@uczelnia.pl", Email = "karolina.szymanska@uczelnia.pl", FirstName = "Karolina", LastName = "Szymańska", Adress = "Adres 12", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "maciej.makowski@uczelnia.pl", Email = "maciej.makowski@uczelnia.pl", FirstName = "Maciej", LastName = "Makowski", Adress = "Adres 13", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "klaudia.maj@uczelnia.pl", Email = "klaudia.maj@uczelnia.pl", FirstName = "Klaudia", LastName = "Maj", Adress = "Adres 14", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "sebastian.sikora@uczelnia.pl", Email = "sebastian.sikora@uczelnia.pl", FirstName = "Sebastian", LastName = "Sikora", Adress = "Adres 15", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "michal.pawlowski@uczelnia.pl", Email = "michal.pawlowski@uczelnia.pl", FirstName = "Michał", LastName = "Pawłowski", Adress = "Adres 16", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "agnieszka.stolarz@uczelnia.pl", Email = "agnieszka.stolarz@uczelnia.pl", FirstName = "Agnieszka", LastName = "Stolarz", Adress = "Adres 17", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "andrzej.zawisza@uczelnia.pl", Email = "andrzej.zawisza@uczelnia.pl", FirstName = "Andrzej", LastName = "Zawisza", Adress = "Adres 18", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "wioletta.tomaszewska@uczelnia.pl", Email = "wioletta.tomaszewska@uczelnia.pl", FirstName = "Wioletta", LastName = "Tomaszewska", Adress = "Adres 19", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "adrian.kulesza@uczelnia.pl", Email = "adrian.kulesza@uczelnia.pl", FirstName = "Adrian", LastName = "Kulesza", Adress = "Adres 20", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "aleksandra.michalowska@uczelnia.pl", Email = "aleksandra.michalowska@uczelnia.pl", FirstName = "Aleksandra", LastName = "Michalowska", Adress = "Adres 21", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "damian.szymanski@uczelnia.pl", Email = "damian.szymanski@uczelnia.pl", FirstName = "Damian", LastName = "Szymański", Adress = "Adres 22", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "joanna.kwiatkowska@uczelnia.pl", Email = "joanna.kwiatkowska@uczelnia.pl", FirstName = "Joanna", LastName = "Kwiatkowska", Adress = "Adres 23", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "krzysztof.jablonski@uczelnia.pl", Email = "krzysztof.jablonski@uczelnia.pl", FirstName = "Krzysztof", LastName = "Jabłoński", Adress = "Adres 24", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "magdalena.kaczmarek@uczelnia.pl", Email = "magdalena.kaczmarek@uczelnia.pl", FirstName = "Magdalena", LastName = "Kaczmarek", Adress = "Adres 25", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "bartosz.kowalski@uczelnia.pl", Email = "bartosz.kowalski@uczelnia.pl", FirstName = "Bartosz", LastName = "Kowalski", Adress = "Adres 26", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "karol.wozniak@uczelnia.pl", Email = "karol.wozniak@uczelnia.pl", FirstName = "Karol", LastName = "Woźniak", Adress = "Adres 27", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "patrycja.ziemba@uczelnia.pl", Email = "patrycja.ziemba@uczelnia.pl", FirstName = "Patrycja", LastName = "Ziemba", Adress = "Adres 28", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "sylwia.makowska@uczelnia.pl", Email = "sylwia.makowska@uczelnia.pl", FirstName = "Sylwia", LastName = "Makowska", Adress = "Adres 29", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "kamil.sawicki@uczelnia.pl", Email = "kamil.sawicki@uczelnia.pl", FirstName = "Kamil", LastName = "Sawicki", Adress = "Adres 30", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "agnieszka.mak@uczelnia.pl", Email = "agnieszka.mak@uczelnia.pl", FirstName = "Agnieszka", LastName = "Mak", Adress = "Adres 31", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "lukasz.pawlowski@uczelnia.pl", Email = "lukasz.pawlowski@uczelnia.pl", FirstName = "Łukasz", LastName = "Pawłowski", Adress = "Adres 32", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "magdalena.wozniak@uczelnia.pl", Email = "magdalena.wozniak@uczelnia.pl", FirstName = "Magdalena", LastName = "Woźniak", Adress = "Adres 33", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "michal.zawisza@uczelnia.pl", Email = "michal.zawisza@uczelnia.pl", FirstName = "Michał", LastName = "Zawisza", Adress = "Adres 34", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "barbara.kwiatkowska@uczelnia.pl", Email = "barbara.kwiatkowska@uczelnia.pl", FirstName = "Barbara", LastName = "Kwiatkowska", Adress = "Adres 35", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "sebastian.michalski@uczelnia.pl", Email = "sebastian.michalski@uczelnia.pl", FirstName = "Sebastian", LastName = "Michalski", Adress = "Adres 36", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "katarzyna.kozlowska@uczelnia.pl", Email = "katarzyna.kozlowska@uczelnia.pl", FirstName = "Katarzyna", LastName = "Kozłowska", Adress = "Adres 37", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "damian.sikora@uczelnia.pl", Email = "damian.sikora@uczelnia.pl", FirstName = "Damian", LastName = "Sikora", Adress = "Adres 38", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "ewelina.michalowska@uczelnia.pl", Email = "ewelina.michalowska@uczelnia.pl", FirstName = "Ewelina", LastName = "Michalowska", Adress = "Adres 39", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "piotr.maj@uczelnia.pl", Email = "piotr.maj@uczelnia.pl", FirstName = "Piotr", LastName = "Maj", Adress = "Adres 40", CreatedAt = DateTime.UtcNow, EmailConfirmed = true }
            };

            var password = "qweASD1231410!";

            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "student");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error creating user {user.Email}: {error.Description}");
                        }
                    }
                }
            }

            // Seed admin user
            string adminEmail = "admin@admin.com";
            string adminPassword = "ADMINadmin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Adress = "Admin",
                    PhoneNumber = "123456789",
                    CreatedAt = DateTime.Now,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating admin user {adminEmail}: {error.Description}");
                    }
                }
            }

            // Seed lecturer users
            var lecturers = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "wykladowca1@uczelnia.pl", Email = "wykladowca1@uczelnia.pl", FirstName = "Lecturer1", LastName = "Lecturer1", Adress = "Adres", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "wykladowca2@uczelnia.pl", Email = "wykladowca2@uczelnia.pl", FirstName = "Lecturer2", LastName = "Lecturer2", Adress = "Adres", CreatedAt = DateTime.UtcNow, EmailConfirmed = true }
            };

            foreach (var lecturer in lecturers)
            {
                var existingLecturer = await userManager.FindByEmailAsync(lecturer.Email);
                if (existingLecturer == null)
                {
                    var result = await userManager.CreateAsync(lecturer, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(lecturer, "lecturer");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error creating lecturer {lecturer.Email}: {error.Description}");
                        }
                    }
                }
            }
            // Seed employee users
            var employees = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "pawel.placzek@uczelnia.pl", Email = "pawel.placzek@uczelnia.pl", FirstName = "Paweł", LastName = "Płaczek", Adress = "Adres 43", CreatedAt = DateTime.UtcNow, EmailConfirmed = true },
                new ApplicationUser { UserName = "wojciech.cellary@uczelnia.pl", Email = "wojciech.cellary@uczelnia.pl", FirstName = "Wojciech", LastName = "Cellary", Adress = "Adres 44", CreatedAt = DateTime.UtcNow, EmailConfirmed = true }
            };

            foreach (var employee in employees)
            {
                var existingLecturer = await userManager.FindByEmailAsync(employee.Email);
                if (existingLecturer == null)
                {
                    var result = await userManager.CreateAsync(employee, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(employee, "employee");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error creating lecturer {employee.Email}: {error.Description}");
                        }
                    }
                }
            }
        }
    }
}
