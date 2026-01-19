using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CandidateDTO
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be 3–100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(?:\+8801|01)[3-9]\d{8}$",
        ErrorMessage = "Invalid Bangladesh phone number")]
        public string Phone { get; set; }
        public string Skills { get; set; } = string.Empty;
        public string CandidateType { get; set; } = "Junior";
        public int ExperienceLevel { get; set; } = 0;
    }
}
