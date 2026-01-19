using DAL.EF;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class JobPostRepository : Repository<JobPost>
    {
        private readonly SJMDbContext _context;
        public JobPostRepository(SJMDbContext context) : base(context) {
            _context = context;
        }


    }
}
