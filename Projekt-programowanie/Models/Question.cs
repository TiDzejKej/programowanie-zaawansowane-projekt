using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektProgramowanie.Models
{
	public class Question
	{
		[Key]
		public int QuestionId { get; set; }

		[Required]
		public string QuestionText { get; set; }

		[Required]
		public int SurveyId { get; set; }

		[ForeignKey("SurveyId")]
		public Survey Survey { get; set; }
	}
}