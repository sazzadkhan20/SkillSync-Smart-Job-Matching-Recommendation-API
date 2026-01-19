using BLL.DTOs;
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
        public CandidateService(DataAccessFactory dataAccess) 
        {
            _dataAccess = dataAccess;
        }

        public CandidateDTO Get(int id)
        {
            var data = _dataAccess.CandidateData().Get(id);
            var result = MapperConfig.GetMapper().Map<CandidateDTO>(data);
            return result;
        }

        public List<CandidateDTO> GetAll()
        {
            var data = _dataAccess.CandidateData().GetAll();
            var result = MapperConfig.GetMapper().Map<List<CandidateDTO>>(data);
            return result;
        }

        public bool Add(CandidateDTO candidate)
        {
            var allData = this.GetAll();
            if (allData != null)
            {
                foreach (var item in allData)
                {
                    if (item.Email.Equals(candidate.Email))
                        throw new Exception("Email Already Register");
                }
            }
            var data = MapperConfig.GetMapper().Map<Candidate>(candidate);
            var result = _dataAccess.CandidateData().Add(data);
            return result;
        }

        public bool Delete(int id)
        {
            var data = _dataAccess.CandidateData().Get(id);
            var result = _dataAccess.CandidateData().Delete(data);
            return result;
        }

        public bool Update(CandidateDTO candidate)
        {
            var data = MapperConfig.GetMapper().Map<Candidate>(candidate);
            var result = _dataAccess.CandidateData().Update(data);
            return result;
        }
    }
}
