using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjektProgramowanie.Models
{
	public class Survey
	{
		[Key]
		public int SurveyId { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public ICollection<Question> Questions { get; set; } = new List<Question>();
	}
}
