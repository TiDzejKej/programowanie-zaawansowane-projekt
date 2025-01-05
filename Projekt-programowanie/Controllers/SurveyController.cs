using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjektProgramowanie.Controllers
{
	[Authorize]
	public class SurveyController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public SurveyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Survey/Create
		public IActionResult Create(int lessonId)
		{
			var userId = _userManager.GetUserId(User);
			
			var model = new SurveyViewModel
			{
				LessonId = lessonId,
                StudentId = userId
			};
			
			return View(model);
		}

		// POST: Survey/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(SurveyViewModel model)
		{
			if (ModelState.IsValid)
			{
				
				var userId = _userManager.GetUserId(User);

				var survey = new Survey
				{
					StudentId = userId, 
					CourseRating = model.CourseRating,
					Recommendation = model.Recommendation,
					LessonId = model.LessonId  
				};

				_context.Surveys.Add(survey);
				_context.SaveChanges();

				return RedirectToAction("Index", "Scheduler");
			}

			return View(model);
		}

		public async Task<IActionResult> Index()
		{
			var userId = _userManager.GetUserId(User);

			IQueryable<Survey> surveysQuery = _context.Surveys;

			if (User.IsInRole("admin"))
			{
				surveysQuery = _context.Surveys;
			}
			else
			{
				surveysQuery = surveysQuery.Where(s => s.StudentId == userId);
			}
			var surveys = await surveysQuery.ToListAsync();
			return View(surveys);
		}
		[HttpGet]
		public IActionResult Detail(int id)
		{
			var survey = _context.Surveys.Find(id);
			if (survey == null)
			{
				return NotFound();
			}
			return View(survey);
		}


		[HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var survey = _context.Surveys.Find(id);
			if (survey != null)
			{
				_context.Surveys.Remove(survey);
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}

	}
}
