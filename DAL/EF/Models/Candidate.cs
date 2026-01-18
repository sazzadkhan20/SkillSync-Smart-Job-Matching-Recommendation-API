using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skills { get; set; } = string.Empty;
        public string CandidateType { get; set; } = "Junior";

        public int ExperienceLevel { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
