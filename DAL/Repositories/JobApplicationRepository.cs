using DAL.EF;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class JobApplicationRepository: Repository<JobApplication>
    {
        private readonly SJMDbContext _context;
        private readonly DbSet<JobApplication> _dbSet;
        public JobApplicationRepository(SJMDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<JobApplication>();
        }

        public override async Task<JobApplication?> GetAsync(int id)
        {
            var application = await (from a in _context.JobApplications.Include(c => c.Candidate).
                               Include(post => post.JobPost)
                               where a.Id == id
                               select a).SingleOrDefaultAsync();
            return application;
        }

        public override async Task<List<JobApplication>> GetAllAsync()
        {
            var applicationList = await (from a in _context.JobApplications.Include(c => c.Candidate).
                               Include(post => post.JobPost)
                               select a).ToListAsync();
            return applicationList;
        }
    }
}
