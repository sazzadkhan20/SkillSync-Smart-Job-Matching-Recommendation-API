using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class JobPostDTO
    {

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be 3–100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Skills are required")]
        public string SkillsRequired { get; set; }

        [Range(0, 30, ErrorMessage = "Experience level must be between 0 and 30 years")]
        public int ExperienceLevel { get; set; } = 0;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(50)]
        public string Location { get; set; }
    }
}
