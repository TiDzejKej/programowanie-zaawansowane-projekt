using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Models;
using System.ComponentModel.DataAnnotations;
using static ProjektProgramowanie.Controllers.ContactFormController;

namespace ProjektProgramowanie.Controllers
{
    public class ContactFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactFormViewmodel model)
        {
            if (ModelState.IsValid)
            {
                var contactForm = new ContactForm
                {
                    Title = model.Title,
                    Content = model.Content,
                    Email = model.Email
                };

                _context.ContactForms.Add(contactForm);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Dziękujemy za przesłanie formularza kontaktowego!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var contactForms = await _context.ContactForms.ToListAsync();
            var contactFormsViewModel = contactForms.Select(c => new ContactFormViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Content = c.Content,
                Email = c.Email
            }).ToList();

            return View(contactFormsViewModel);
        }
        public class ContactFormViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Email { get; set; }
        }

        public class ContactFormViewmodel
        {
            [Required]
            public string Title { get; set; }

            [Required]
            public string Content { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

    }
}
