using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class JobApplicationDTO
    {
        [Required(ErrorMessage = "Candidate Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Candidate Id must be a valid positive number")]
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Job Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Job Id must be a valid positive number")]
        public int JobId { get; set; }
    }
}
