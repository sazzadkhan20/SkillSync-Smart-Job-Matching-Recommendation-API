using Common;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IJobApplicationFeature<T> where T : JobApplication
    {
        public Task<List<JobApplication>> GetByJobId(int id);

        public Task<List<JobApplication>?> GetByCandiadteId(int id);

        public Task<JobApplication?> GetByCandiadteAndJobId(int cId, int jId);

        public Task<List<Candidate>?> GetSelectedCandidate(int id,int skillMatchValue, string decision);
    }
}
