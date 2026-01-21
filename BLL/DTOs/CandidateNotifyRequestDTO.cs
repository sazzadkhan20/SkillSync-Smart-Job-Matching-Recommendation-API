using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CandidateNotifyRequestDTO
    {

        [Required(ErrorMessage = "MinimumSkillMatch is required")]
        [Range(40, 100, ErrorMessage = "Skill match must be between 40 and 100")]
        public int MinimumSkillMatch { get; set; }

        [Required(ErrorMessage = "DecisionType is required")]
        public string DecisionType { get; set; }
    }
}
