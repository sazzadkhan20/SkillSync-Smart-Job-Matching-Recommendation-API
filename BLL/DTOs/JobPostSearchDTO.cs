using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class JobPostSearchDTO: JobPostDTO
    {
        public DateTime? PostedAt { get; set; }
    }
}
