using BLL.DTOs;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainLogic.Calculators
{
    public interface ISkillMatchCalculator
    {
        public int Calculate(Candidate candidate, JobPost jobPost);

        public int Calculate(JobPostSearchDTO post, string? title, HashSet<string> candidateSkillSet,
            int minExperience, string? location, DateTime? postedAfter);
    }
}
