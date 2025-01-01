using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class SurveyViewModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "LessonId is required.")]
		public int LessonId { get; set; }

		[Required(ErrorMessage = "StudentId is required.")]
		[StringLength(450, ErrorMessage = "StudentId cannot exceed 450 characters.")]
		public string StudentId { get; set; }

		[Required(ErrorMessage = "CourseRating is required.")]
		[Range(1, 10, ErrorMessage = "CourseRating must be between 1 and 10.")]
		public int CourseRating { get; set; }

		[Required(ErrorMessage = "Recommendation is required.")]
		[StringLength(500, ErrorMessage = "Recommendation cannot exceed 500 characters.")]
		public string Recommendation { get; set; }
	}
}

