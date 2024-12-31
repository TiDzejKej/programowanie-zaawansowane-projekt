using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektProgramowanie.Models
{
	[NotMapped]// EF Ignore
	public class AnswerViewModel
	{
		public int SurveyId { get; set; }

		public List<Question> Questions { get; set; }
		
		public Dictionary<int, string> Responses { get; set; } = new Dictionary<int, string>();
	}
}
