using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;

namespace ProjektProgramowanie.Controllers
{
	public class QuestionsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<QuestionsController> _logger;

		public QuestionsController(ApplicationDbContext context, ILogger<QuestionsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		// GET: Questions
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Question.Include(q => q.Survey);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Questions/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var question = await _context.Question
				.Include(q => q.Survey)
				.FirstOrDefaultAsync(m => m.QuestionId == id);
			if (question == null)
			{
				return NotFound();
			}

			return View(question);
		}

		// GET: Questions/Create
		public IActionResult Create()
		{
			// Pobranie wszystkich Survey
			var surveys = _context.Survey.ToList();

			// Ustawienie ViewData do przekazania SelectList do widoku
			ViewData["SurveyId"] = new SelectList(surveys, "SurveyId", "Name");

			// Pobranie SelectList z ViewData
			var selectList = ViewData["SurveyId"] as SelectList;
			if (selectList != null)
			{
				// Wypisanie każdego elementu SelectList w konsoli
				Console.WriteLine("Lista SurveyId:");
				foreach (var item in selectList)
				{
					Console.WriteLine($"SurveyId: {item.Value}, Name: {item.Text}");
				}

				// Dodatkowo, wypisanie wszystkich SurveyId z listy
				Console.WriteLine("Lista wszystkich SurveyId:");
				foreach (var survey in surveys)
				{
					Console.WriteLine($"SurveyId: {survey.SurveyId}, Name: {survey.Name}");
				}
			}

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("QuestionId,QuestionText,SurveyId")] Question question)
		{
			_logger.LogInformation("Creating question with data: {@Question}", question);

			// Walidacja ModelState
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				// Zapisz lub przejrzyj `errors` w debuggerze
			}

			if (ModelState.IsValid)
			{
				// Dodanie pytania do bazy danych
				_context.Add(question);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				_logger.LogWarning("ModelState is invalid. Errors: {@ModelStateErrors}", errors);
			}

			// Ustawienie ViewData z poprawionym SurveyId po wysłaniu formularza
			ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Name", question.SurveyId);
			return View(question);
		}


		// GET: Questions/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var question = await _context.Question.FindAsync(id);
			if (question == null)
			{
				return NotFound();
			}
			ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Name", question.SurveyId);
			return View(question);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("QuestionId,QuestionText,SurveyId")] Question question)
		{
			if (id != question.QuestionId)
			{
				return NotFound();
			}

			_logger.LogInformation("Editing question with data: {@Question}", question);

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(question);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!QuestionExists(question.QuestionId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			else
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				_logger.LogWarning("ModelState is invalid. Errors: {@ModelStateErrors}", errors);
			}
			ViewData["SurveyId"] = new SelectList(_context.Survey, "SurveyId", "Name", question.SurveyId);
			return View(question);
		}

		// GET: Questions/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var question = await _context.Question
				.Include(q => q.Survey)
				.FirstOrDefaultAsync(m => m.QuestionId == id);
			if (question == null)
			{
				return NotFound();
			}

			return View(question);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var question = await _context.Question.FindAsync(id);
			if (question != null)
			{
				_context.Question.Remove(question);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool QuestionExists(int id)
		{
			return _context.Question.Any(e => e.QuestionId == id);
		}
	}
}
