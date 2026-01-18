using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class JobApplication
    {

        public int Id { get; set; }

        public string ApplicationStatus { get; set; } = "Applied";

        public int SkillMatchPercent { get; set; } = 0;

        public DateTime AppliedDate { get; set; } = DateTime.Now;

        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [ForeignKey("JobPost")]
        public int JobId { get; set; }

        public virtual JobPost JobPost { get; set; }
    }
}
