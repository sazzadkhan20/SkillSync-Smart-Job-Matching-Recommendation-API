using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JobApplicationService
    {
        private readonly DataAccessFactory _dataAccess;
        private readonly ISkillMatchCalculator _skillMatch;
        public JobApplicationService(DataAccessFactory dataAccess,ISkillMatchCalculator skillMatch)
        {
            _dataAccess = dataAccess;
            _skillMatch = skillMatch;
        }
        public JobApplicationDetailsDTO Get(int id)
        {
            var data = _dataAccess.JobApplicationData().Get(id);
            var result = MapperConfig.GetMapper().Map<JobApplicationDetailsDTO>(data);
            return result;
        }

        public List<JobApplicationDetailsDTO> GetAll()
        {
            var data = _dataAccess.JobApplicationData().GetAll();
            var result = MapperConfig.GetMapper().Map<List<JobApplicationDetailsDTO>>(data);
            return result;
        }

        public bool Add(JobApplicationDTO application)
        {
            var candidateData = _dataAccess.CandidateData().Get(application.CandidateId);
            var jobData = _dataAccess.JobPostData().Get(application.JobId);
            if (candidateData == null)
            {
                throw new Exception("Invalid Candidate id");
            }
            if (jobData == null)
            {
                throw new Exception("Invalid Job id");
            }
            int skillMatchPErcentage = _skillMatch.Calculate(candidateData, jobData);
            var data = MapperConfig.GetMapper().Map<JobApplication>(application);
            data.SkillMatchPercent = skillMatchPErcentage;
            var result = _dataAccess.JobApplicationData().Add(data);
            return result;
        }

        public bool Delete(int id)
        {
            var data = _dataAccess.JobApplicationData().Get(id);
            var result = _dataAccess.JobApplicationData().Delete(data);
            return result;
        }

        public bool Update(JobApplicationDTO application)
        {
            var data = MapperConfig.GetMapper().Map<JobApplication>(application);
            var result = _dataAccess.JobApplicationData().Update(data);
            return result;
        }
    }
}
