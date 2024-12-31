using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektProgramowanie.Models
{
	public class Question
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string QuestionText { get; set; }

		public int SurveyId { get; set; }

		public virtual Survey Survey { get; set; }
	}
}
