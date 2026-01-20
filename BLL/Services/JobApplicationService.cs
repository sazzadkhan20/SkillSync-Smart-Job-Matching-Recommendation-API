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
        public async Task<JobApplicationDetailsDTO> GetAsync(int id)
        {
            var data = await _dataAccess.JobApplicationData().GetAsync(id);
            var result = MapperConfig.GetMapper().Map<JobApplicationDetailsDTO>(data);
            return result;
        }

        public async Task<List<JobApplicationDetailsDTO>> GetAllAsync()
        {
            var data = await _dataAccess.JobApplicationData().GetAllAsync();
            var result = MapperConfig.GetMapper().Map<List<JobApplicationDetailsDTO>>(data);
            return result;
        }

        public async Task<bool> AddAsync(JobApplicationDTO application)
        {
            var candidateData = await _dataAccess.CandidateData().GetAsync(application.CandidateId);
            var jobData = await _dataAccess.JobPostData().GetAsync(application.JobId);
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
            var result = await _dataAccess.JobApplicationData().AddAsync(data);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _dataAccess.JobApplicationData().GetAsync(id);
            if (data == null)
            {
                throw new Exception("Job Application Not Found");
            }
            var result = await _dataAccess.JobApplicationData().DeleteAsync(data);
            return result;
        }

        public async Task<bool> UpdateAsync(JobApplicationDTO application, int id)
        {
            var existingData = await _dataAccess.JobApplicationData().GetAsync(id);
            if (existingData == null)
            {
                throw new Exception("Job Application Not Found");
            }
            var data = MapperConfig.GetMapper().Map<JobApplication>(application);
            var result = await _dataAccess.JobApplicationData().UpdateAsync(data);
            return result;
        }
    }
}
