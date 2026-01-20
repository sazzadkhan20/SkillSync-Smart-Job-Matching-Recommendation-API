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
    }
}
