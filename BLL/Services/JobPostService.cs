using BLL.DomainLogic.Converters;
using BLL.DomainLogic.Specifications;
using BLL.DTOs;
using BLL.Helpers;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JobPostService
    {
        private readonly DataAccessFactory _dataAccess;
        private readonly JobPostSpecification _specification;
        public JobPostService(DataAccessFactory dataAccess,JobPostSpecification specification)
        {
            _dataAccess = dataAccess;
            _specification = specification;
        }

        public async Task<JobPostDTO> GetAsync(int id)
        {
            var data = await _dataAccess.JobPostData().GetAsync(id);
            var result = MapperConfig.GetMapper().Map<JobPostDTO>(data);
            return result;
        }

        public async Task<List<JobPostDTO>> GetAllAsync()
        {
            var data = await _dataAccess.JobPostData().GetAllAsync();
            var result = MapperConfig.GetMapper().Map<List<JobPostDTO>>(data);
            return result;
        }

        public async Task<bool> AddAsync(JobPostDTO JobPost)
        {
            var data = MapperConfig.GetMapper().Map<JobPost>(JobPost);
            var result = await _dataAccess.JobPostData().AddAsync(data);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _dataAccess.JobPostData().GetAsync(id);
            if (data == null)
            {
                throw new Exception("Job Post Not Found");
            }
            var result = await _dataAccess.JobPostData().DeleteAsync(data);
            return result;
        }

        public async Task<bool> UpdateAsync(JobPostDTO JobPost, int id)
        {
            var existingData = await _dataAccess.JobPostData().GetAsync(id);
            if (existingData == null)
            {
                throw new Exception("Job Post Not Found");
            }
            var data = MapperConfig.GetMapper().Map<JobPost>(JobPost);
            var result = await _dataAccess.JobPostData().UpdateAsync(data);
            return result;
        }

        public async Task<JobPostWithApplicationDTO> GetJobSpecificDetailsAsync(int id)
        {
            var post = await _dataAccess.JobPostData().GetAsync(id);
            if (post == null)
                throw new Exception("Job Post Not Found");
            var data = await _dataAccess.JobApplicationFeature().GetByJobId(id);
            if(data == null || data.Count == 0)
                throw new Exception("No Applications Found for this Job Post");
            var result = MapperConfig.mapper(data,post.PostedAt);
            return result;
        }

        public async Task<List<JobPostSearchDTO>> SearchAsync(string? title,
            string[]? skills,int minExperience,string? location,DateTime? postedAfter,string sort)
        {
            var jobPosts = await _dataAccess.JobPostData().GetAllAsync();
            if(jobPosts == null || jobPosts.Count == 0)
            {
                throw new Exception("No Job Posts Found");
            }
            var data = MapperConfig.GetMapper().Map<List<JobPostSearchDTO>>(jobPosts);
            var result = _specification.Filter(data, title, skills, minExperience, location, postedAfter);
            if(result == null || result.Count == 0)
            {
                throw new Exception("No Job Posts Found Matching the Criteria");
            }
            // Sort the result based on PostedAt
            Converter _converter = new Converter();
            result = _converter.sortByDate(result, sort);
            return result;
        }

        // Search Job Applications by Status for a specific Job Post
        public async Task<JobPostWithApplicationDTO?> SearchByStatusAsync(int id,string? status)
        {
            var data = await this.GetJobSpecificDetailsAsync(id);
            if(status == null)
            {
                return data;
            }
            else if(status.ToLower() == "all")
            {
                return data;
            }
            else
            {
                var filteredApplications = data.JobApplications.Where(map => map.ApplicationStatus
                        .Trim().ToLower().Equals(status.Trim().ToLower())
                        ).ToList();
                data.JobApplications = filteredApplications;
                if(data.JobApplications == null || data.JobApplications.Count == 0)
                {
                    throw new Exception("No Applications Found with the given Status");
                }
                return data;
            }
        }
    }
}
