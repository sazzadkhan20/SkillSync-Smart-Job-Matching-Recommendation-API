using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class JobSpecificApplicationDetailsDTO
    {
        public JobSpecificApplicationDetailsDTO()
        {
            Candidate = new CandidateDTO();
        }
        public string ApplicationStatus { get; set; }

        public int SkillMatchPercent { get; set; }

        public DateTime AppliedDate { get; set; }

        // Candidate Details
        public CandidateDTO Candidate { get; set; }
    }
}
