using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;

namespace ProjektProgramowanie.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchedulerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _context.Groups
                .Include(g => g.Teacher) 
                .Include(g => g.Students)
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    Teacher = $"{g.Teacher.FirstName} {g.Teacher.LastName}",
                    Students = g.Students.Select(s => $"{s.FirstName} {s.LastName}")
                })
                .ToListAsync();

            return Ok(groups);
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _context.Lessons
                .Include(l => l.Group)
                .ThenInclude(g => g.Teacher)
                .Include(l => l.Group.Students)
                .ToListAsync();

            var scheduleData = lessons.Select(l => new
            {
                Id = l.Id,
                Subject = l.Title,
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                Description = l.Description,
                GroupName = l.Group.Name,
                Teacher = $"{l.Group.Teacher.FirstName} {l.Group.Teacher.LastName}",
                Students = l.Group.Students.Select(s => $"{s.FirstName} {s.LastName}")
            });

            return View(scheduleData);
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson([FromBody] LessonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var group = await _context.Groups.FindAsync(model.GroupId);
            if (group == null)
            {
                return NotFound("Group not found");
            }

            // Dodaj nowe zajęcia
            var lesson = new Lesson
            {
                Title = model.Title,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Description = model.Description,
                GroupId = model.GroupId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Dodano nowe zajęcia!" });
        }

        public class LessonViewModel
        {
            public string Title { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Description { get; set; }
            public int GroupId { get; set; } 
        }
    }
}
