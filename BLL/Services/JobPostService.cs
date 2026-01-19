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

        public JobPostDTO Get(int id)
        {
            var data = _dataAccess.JobPostData().Get(id);
            var result = MapperConfig.GetMapper().Map<JobPostDTO>(data);
            return result;
        }

        public List<JobPostDTO> GetAll()
        {
            var data = _dataAccess.JobPostData().GetAll();
            var result = MapperConfig.GetMapper().Map<List<JobPostDTO>>(data);
            return result;
        }

        public bool Add(JobPostDTO JobPost)
        {
            var data = MapperConfig.GetMapper().Map<JobPost>(JobPost);
            var result = _dataAccess.JobPostData().Add(data);
            return result;
        }

        public bool Delete(int id)
        {
            var data = _dataAccess.JobPostData().Get(id);
            var result = _dataAccess.JobPostData().Delete(data);
            return result;
        }

        public bool Update(JobPostDTO JobPost)
        {
            var data = MapperConfig.GetMapper().Map<JobPost>(JobPost);
            var result = _dataAccess.JobPostData().Update(data);
            return result;
        }
    }
}
