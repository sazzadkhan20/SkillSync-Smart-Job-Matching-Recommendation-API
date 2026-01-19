using BLL.Interfaces;
using DAL.EF.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainLogic
{
    public class SkillMatchCalculator: ISkillMatchCalculator
    {

        // Remove Duplicate
        private HashSet<string> SkillSet(string skillSet)
        {
            if (string.IsNullOrWhiteSpace(skillSet))
                return new HashSet<string>();
            return new HashSet<string>(skillSet.Split(',',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                StringComparer.OrdinalIgnoreCase);
        }
        public int Calculate(Candidate candidate,JobPost jobPost)
        {
            HashSet<string> candidateSkillSet = SkillSet(candidate.Skills);
            HashSet<string> jobSkillSet = SkillSet(jobPost.SkillsRequired);
            int matchCount = candidateSkillSet.Intersect(jobSkillSet).Count(); // remove duplicate count
            if (candidate.ExperienceLevel >= jobPost.ExperienceLevel)
                matchCount++;
            if (jobPost.Title.Trim().ToLower().Contains(candidate.CandidateType.Trim().ToLower()))
                matchCount++;
            return (int)Math.Ceiling((double)matchCount / (jobSkillSet.Count() + 2)*100);
        }
    }
}
