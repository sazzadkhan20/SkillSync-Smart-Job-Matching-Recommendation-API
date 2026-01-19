using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class JobApplicationDetailsDTO
    {
        public JobApplicationDetailsDTO()
        {
            Candidate = new CandidateDTO();
            JobPost = new JobPostDTO();
        }
        public string ApplicationStatus { get; set; }

        public int SkillMatchPercent { get; set; }

        public DateTime AppliedDate { get; set; }

        // Candidate Details
        public CandidateDTO Candidate { get; set; }

        // Job Post Details
        public JobPostDTO JobPost { get; set; }
    }
}
