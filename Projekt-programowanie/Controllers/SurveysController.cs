using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;


namespace ProjektProgramowanie.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Survey.ToListAsync());
        }
		public IActionResult FillSurvey(int id)
		{
			var survey = _context.Survey
								 .Include(s => s.Questions)
								 .FirstOrDefault(s => s.SurveyId == id);

			if (survey == null) return NotFound();

			var viewModel = new AnswerViewModel
			{
				SurveyId = survey.SurveyId,
				Questions = survey.Questions.ToList()
			};

			return View(viewModel);
		}

		// Przesyłanie odpowiedzi
		[HttpPost]
		public IActionResult SubmitSurvey(AnswerViewModel model)
		{
			foreach (var response in model.Responses)
			{
				var answer = new Answer
				{
					SurveyId = model.SurveyId,
					QuestionId = response.Key,
					Response = response.Value,
					UserId = User.Identity?.Name // Zakładamy, że User.Identity.Name zawiera identyfikator użytkownika
				};
				_context.Answer.Add(answer);
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}
    
        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

		// GET: Surveys/Create
		// POST: Surveys/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("SurveyId, Name, Description")] Survey survey)
		{
			if (ModelState.IsValid)
			{
				_context.Add(survey);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			// W przypadku błędów walidacji, wyświetlamy ponownie formularz
			return View(survey);
		}

		// GET: Surveys/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyId,Name,Description")] Survey survey)
        {
            if (id != survey.SurveyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.SurveyId))
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
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _context.Survey.FindAsync(id);
            if (survey != null)
            {
                _context.Survey.Remove(survey);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.SurveyId == id);
        }
    }
}
