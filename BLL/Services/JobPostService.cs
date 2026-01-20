using BLL.DTOs;
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
        public JobPostService(DataAccessFactory dataAccess)
        {
            _dataAccess = dataAccess;
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

        public async Task<bool> UpdateAsync(JobPostDTO JobPost,int id)
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
    }
}
