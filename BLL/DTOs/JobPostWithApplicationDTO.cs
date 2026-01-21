using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class JobPostWithApplicationDTO: JobPostDTO
    {
        public JobPostWithApplicationDTO()
        {
            JobApplications = new List<JobSpecificApplicationDetailsDTO>();
        }
        public DateTime PostedAt { get; set; }

        public List<JobSpecificApplicationDetailsDTO> JobApplications { get; set; }
    }
}
