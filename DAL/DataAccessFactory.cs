using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class DataAccessFactory
    {
        private readonly SJMDbContext _context;
        public DataAccessFactory(SJMDbContext context) {
            _context = context;
        }

        public IRepository<Candidate> CandidateData()
        {
            return new CandidateRepository(_context);
        }

        public IRepository<JobPost> JobPostData()
        {
            return new JobPostRepository(_context);
        }

        //public IJobPostFeature<JobPost> JobPostFeature()
        //{
        //    return new JobPostRepository(_context);
        //}

        public IRepository<JobApplication> JobApplicationData()
        {
            return new JobApplicationRepository(_context);
        }

        public IJobApplicationFeature<JobApplication> JobApplicationFeature()
        {
            return new JobApplicationRepository(_context);
        }
    }
}
