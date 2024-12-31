using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Controllers
{
    [Authorize(Roles = "admin, employee")]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Students)
                .ToListAsync();

            return View(groups);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            var teachers = await _userManager.GetUsersInRoleAsync("lecturer");
            var students = await _userManager.GetUsersInRoleAsync("student");

            var model = new GroupEditViewModel
            {
                Id = group.Id,
                Name = group.Name,
                TeacherId = group.TeacherId,
                SelectedStudentIds = group.Students.Select(s => s.Id).ToList(),
                Teachers = teachers.Select(t => new SelectListItem
                {
                    Value = t.Id,
                    Text = $"{t.FirstName} {t.LastName}"
                }),
                Students = students.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = $"{s.FirstName} {s.LastName}"
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = await _context.Groups
                    .Include(g => g.Students)
                    .FirstOrDefaultAsync(g => g.Id == model.Id);

                if (group == null)
                {
                    return NotFound();
                }

                group.Name = model.Name;
                group.TeacherId = model.TeacherId;
                group.Students = _context.Users.Where(u => model.SelectedStudentIds.Contains(u.Id)).ToList();

                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Students)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("lecturer");
            var students = await _userManager.GetUsersInRoleAsync("student");

            var model = new GroupEditViewModel
            {
                Teachers = teachers.Select(t => new SelectListItem
                {
                    Value = t.Id,
                    Text = $"{t.FirstName} {t.LastName}"
                }),
                Students = students.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = $"{s.FirstName} {s.LastName}"
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = new Group
                {
                    Name = model.Name,
                    TeacherId = model.TeacherId,
                    Students = _context.Users.Where(u => model.SelectedStudentIds.Contains(u.Id)).ToList()
                };

                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload teacher and student lists in case of errors
            var teachers = await _userManager.GetUsersInRoleAsync("lecturer");
            var students = await _userManager.GetUsersInRoleAsync("student");

            model.Teachers = teachers.Select(t => new SelectListItem
            {
                Value = t.Id,
                Text = $"{t.FirstName} {t.LastName}"
            });
            model.Students = students.Select(s => new SelectListItem
            {
                Value = s.Id,
                Text = $"{s.FirstName} {s.LastName}"
            });

            return View(model);
        }

        

    }
}
