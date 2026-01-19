using BLL.DTOs;
using DAL;
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
        public JobApplicationService(DataAccessFactory dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public JobApplicationDTO Get(int id)
        {
            var data = _dataAccess.JobApplicationData().Get(id);
            var result = MapperConfig.GetMapper().Map<JobApplicationDTO>(data);
            return result;
        }

        public List<JobApplicationDTO> GetAll()
        {
            var data = _dataAccess.JobApplicationData().GetAll();
            var result = MapperConfig.GetMapper().Map<List<JobApplicationDTO>>(data);
            return result;
        }

        public bool Add(JobApplicationDTO JobApplication)
        {
            var data = MapperConfig.GetMapper().Map<JobApplication>(JobApplication);
            var result = _dataAccess.JobApplicationData().Add(data);
            return result;
        }

        public bool Delete(int id)
        {
            var data = _dataAccess.JobApplicationData().Get(id);
            var result = _dataAccess.JobApplicationData().Delete(data);
            return result;
        }

        public bool Update(JobApplicationDTO JobApplication)
        {
            var data = MapperConfig.GetMapper().Map<JobApplication>(JobApplication);
            var result = _dataAccess.JobApplicationData().Update(data);
            return result;
        }
    }
}
