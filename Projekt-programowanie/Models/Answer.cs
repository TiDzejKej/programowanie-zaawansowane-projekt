using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class Answer
	{
		[Key]
		public int AnswerId { get; set; }

		public int SurveyId { get; set; } // Klucz obcy do Survey
		[ForeignKey("SurveyId")]
		public Survey Survey { get; set; }

		public int QuestionId { get; set; } // Klucz obcy do Question
		[ForeignKey("QuestionId")]
		public Question Question { get; set; }

		public string Response { get; set; } // Odpowiedź użytkownika
		public string UserId { get; set; } // Id użytkownika, jeżeli chcesz śledzić, kto odpowiedział
	}
}
