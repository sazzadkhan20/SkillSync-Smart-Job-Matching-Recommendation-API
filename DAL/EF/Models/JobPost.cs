using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class JobPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string SkillsRequired { get; set; }

        public int ExperienceLevel { get; set; } = 0;
        public string Location { get; set; } = "Dhaka";
        public bool PostStatus { get; set; } = true;
        public DateTime PostedAt { get; set; } = DateTime.Now;
    }
}
