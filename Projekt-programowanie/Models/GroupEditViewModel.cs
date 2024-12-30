using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProjektProgramowanie.Models
{
    public class GroupEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public List<string> SelectedStudentIds { get; set; } = new List<string>();

        public IEnumerable<SelectListItem> Teachers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Students { get; set; } = new List<SelectListItem>();
    }
}
