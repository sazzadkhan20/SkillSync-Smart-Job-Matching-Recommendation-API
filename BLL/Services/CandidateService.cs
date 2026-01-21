using BLL.DomainLogic.Calculators;
using BLL.DomainLogic.Converters;
using BLL.DTOs;
using BLL.Helpers;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Services
{
    public class CandidateService
    {
        private readonly DataAccessFactory _dataAccess;
        private readonly ISkillMatchCalculator _skillMatch;
        public CandidateService(DataAccessFactory dataAccess,ISkillMatchCalculator skillMatch) 
        {
            _dataAccess = dataAccess;
            _skillMatch = skillMatch;
        }

        public async Task<CandidateDTO> GetAsync(int id)
        {
            var data = await _dataAccess.CandidateData().GetAsync(id);
            var result = MapperConfig.GetMapper().Map<CandidateDTO>(data);
            return result;
        }

        public async Task<List<CandidateDTO>> GetAllAsync()
        {
            var data = await _dataAccess.CandidateData().GetAllAsync();
            var result = MapperConfig.GetMapper().Map<List<CandidateDTO>>(data);
            return result;
        }

        public async Task<bool> AddAsync(CandidateDTO candidate)
        {
            var allData = await this.GetAllAsync();
            if (allData != null)
            {
                foreach (var item in allData)
                {
                    if (item.Email.Equals(candidate.Email))
                        throw new Exception("Email Already Register");
                }
            }
            var data = MapperConfig.GetMapper().Map<Candidate>(candidate);
            var result = await _dataAccess.CandidateData().AddAsync(data);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _dataAccess.CandidateData().GetAsync(id);
            if(data == null)
            {
                throw new Exception("Candidate Not Found");
            }
            var result = await _dataAccess.CandidateData().DeleteAsync(data);
            return result;
        }

        public async Task<bool> UpdateAsync(CandidateDTO candidate,int id)
        {
            var existingCandidate = await _dataAccess.CandidateData().GetAsync(id);
            if (existingCandidate == null)
            {
                throw new Exception("Candidate Not Found");
            }
            var data = MapperConfig.GetMapper().Map<Candidate>(candidate);
            var result = await _dataAccess.CandidateData().UpdateAsync(data);
            return result;
        }

        public async Task<List<JobPostSearchDTO>> GetRecommendedJobsAsync(int id,int filter,string sort)
        {
            var candidate = await _dataAccess.CandidateData().GetAsync(id);
            if (candidate == null)
            {
                throw new Exception("Candidate Not Found");
            }
            var allJobPosts = await _dataAccess.JobPostData().GetAllAsync();
            List<JobPostSearchDTO> mappedJobPost = new List<JobPostSearchDTO>();
            if(filter>100) filter = 100;
            foreach (var jobPost in allJobPosts)
                if(_skillMatch.Calculate(candidate,jobPost) >= filter) mappedJobPost.Add(MapperConfig.GetMapper().Map<JobPostSearchDTO>(jobPost));
            // Sort the result based on PostedAt
            Converter _converter = new Converter();
            mappedJobPost = _converter.sortByDate(mappedJobPost, sort);
            return mappedJobPost;
        }
    }
}
