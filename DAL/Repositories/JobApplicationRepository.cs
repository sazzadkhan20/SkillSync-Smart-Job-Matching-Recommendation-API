using Common;
using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class JobApplicationRepository: Repository<JobApplication>, IJobApplicationFeature<JobApplication>
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

        public async Task<List<JobApplication>> GetByJobId(int id)
        {
            var applications = await (from a in _context.JobApplications
                                      .Include(c => c.Candidate)
                                      .Include(post => post.JobPost)
                                      where a.JobId == id
                                      select a).ToListAsync();
            return applications;
        }

        public async Task<List<JobApplication>?> GetByCandiadteId(int id)
        {
            var applications = await (from a in _context.JobApplications
                                      .Include(c => c.Candidate)
                                      .Include(post => post.JobPost)
                                      where a.CandidateId == id
                                      select a).ToListAsync();
            return applications;
        }

        public async Task<JobApplication?> GetByCandiadteAndJobId(int cId,int jId)
        {
            var applications = await (from a in _context.JobApplications
                                      .Include(c => c.Candidate)
                                      .Include(post => post.JobPost)
                                      where a.CandidateId == cId && a.JobId == jId
                                      select a).FirstOrDefaultAsync();
            return applications;
        }

        public async Task<List<Candidate>?> GetSelectedCandidate(int id, int skillMatchValue, string decision)
        {
            var applications = this.GetByJobId(id);
            if(applications == null)
            {
                throw new Exception("No applications found for the given job id.");
            }
            List<Candidate> candidates = new List<Candidate>();
            foreach(var app in applications.Result)
            {
                if (app.SkillMatchPercent >= skillMatchValue &&
                    ((decision.Equals("Interview") && app.ApplicationStatus.Equals("Applied")) ||
                    (decision.Equals("Hired") && app.ApplicationStatus.Equals("Selected"))))
                {
                    candidates.Add(app.Candidate);
                    if(decision.Equals("Interview"))
                        app.ApplicationStatus = "Selected";
                    else 
                        app.ApplicationStatus = "Hired";
                    _context.JobApplications.Update(app);
                    await _context.SaveChangesAsync();
                }
            }
            if (candidates.Count == 0)
            {
                throw new Exception("No candidates found matching the criteria.");
            }
            return candidates;
        }
    }
}
