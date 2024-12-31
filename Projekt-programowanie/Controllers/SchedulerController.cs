﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;

namespace ProjektProgramowanie.Controllers
{
    [Authorize]
    public class SchedulerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchedulerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin, employee, lecturer")]
        public async Task<IActionResult> GetGroups()
        {
            var userId = _userManager.GetUserId(User);

            IQueryable<Group> groupsQuery = _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Students);

            // Filtr dla roli Lecturer
            if (User.IsInRole("lecturer"))
            {
                groupsQuery = groupsQuery.Where(g => g.TeacherId == userId);
            }

            var groups = await groupsQuery
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

        public IActionResult Index()
        {
            return View("Index", GetRole());
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        public async Task<IActionResult> AddLesson([FromBody] LessonViewModel model)
        {
            var utcPlusOneStart = model.StartTime.AddHours(1);
            var utcPlusOneEnd = model.EndTime.AddHours(1);

            var lesson = new Lesson
            {
                Title = model.Title,
                StartTime = utcPlusOneStart,
                EndTime = utcPlusOneEnd,
                Description = model.Description,
                GroupId = model.GroupId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Lesson added successfully!" });
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        public async Task<IActionResult> UpdateLesson([FromBody] LessonViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Invalid request data." });
            }
            var lesson = await _context.Lessons.FindAsync(model.Id);
            if (lesson == null)
            {
                return Json(new { success = false, message = "Lesson not found." });
            }

            var utcPlusOneStart = model.StartTime.AddHours(1);
            var utcPlusOneEnd = model.EndTime.AddHours(1);

            lesson.Title = model.Title;
            lesson.StartTime = utcPlusOneStart;
            lesson.EndTime = utcPlusOneEnd;
            lesson.Description = model.Description;
            lesson.GroupId = model.GroupId;

            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Lesson updated successfully!" });
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        public async Task<IActionResult> ValidateLesson([FromBody] LessonValidationViewModel model)
        {
            if(model == null)
            {
                return Json(new { success = false, message = "Invalid data!" });
            }
            model.StartTime = model.StartTime.AddHours(1);
            model.EndTime = model.EndTime.AddHours(1);

            var conflictingLessons = await _context.Lessons
                .Where(l => l.GroupId == model.GroupId && l.Id != model.Id &&
                            ((model.StartTime >= l.StartTime && model.StartTime < l.EndTime) ||
                             (model.EndTime > l.StartTime && model.EndTime <= l.EndTime)))
                .ToListAsync();

            if (conflictingLessons.Any())
            {
                return Json(new { success = false, message = "Time conflict detected for the same group." });
            }
            var groupExists = await _context.Groups.AnyAsync(g => g.Id == model.GroupId);
            if (!groupExists)
            {
                return Json(new { success = false, message = "Invalid GroupId. The group does not exist." });
            }

            return Json(new { success = true });
        }
        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        public async Task<IActionResult> DeleteLesson([FromBody] LessonDeleteViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Invalid request data." });
            }
            var lesson = await _context.Lessons.FindAsync(model.Id);

            if (lesson == null)
            {
                return Json(new { success = false, message = "Lesson not found." });
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Lesson deleted successfully." });
        }

        public class LessonDeleteViewModel
        {
            public int Id { get; set; }
        }


        public class LessonValidationViewModel
        {
            public int? Id { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int GroupId { get; set; }
        }


        [HttpGet]
        [Authorize(Roles = "admin, employee, lecturer")]
        public async Task<IActionResult> GetLessons()
        {
            var userId = _userManager.GetUserId(User);

            IQueryable<Lesson> lessonsQuery = _context.Lessons
                .Include(l => l.Group)
                .ThenInclude(g => g.Teacher);

            if (User.IsInRole("lecutrer"))
            {
                lessonsQuery = lessonsQuery.Where(l => l.Group.TeacherId == userId);
            }

            var lessons = await lessonsQuery.ToListAsync();

            var result = lessons.Select(l => new
            {
                Id = l.Id,
                Title = l.Title,
                StartTime = l.StartTime,
                EndTime = l.EndTime,
                Description = l.Description,
                GroupId = l.GroupId,
                GroupName = l.Group.Name,
                Teacher = $"{l.Group.Teacher.FirstName} {l.Group.Teacher.LastName}"
            });

            return Json(result);
        }

        public class LessonViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Description { get; set; }
            public int GroupId { get; set; }
        }

        public string GetRole()
        {
            return User.IsInRole("admin") ? "admin" : User.IsInRole("employee") ? "employee" : "lecturer";
        }
    }
}
