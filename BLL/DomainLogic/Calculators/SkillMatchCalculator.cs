using BLL.DomainLogic.Converters;
using BLL.DTOs;
using DAL.EF.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainLogic.Calculators
{
    public class SkillMatchCalculator: ISkillMatchCalculator
    {

        public int SkillCountCalculate(HashSet<string> candidateSkillSet, HashSet<string> jobSkillSet)
        {
            int matchCount = 0;
            // match skills with partial matches
            foreach (var skill in candidateSkillSet)
            {
                foreach (var jobSkill in jobSkillSet)
                {
                    if (skill.Trim().ToLower().Contains(jobSkill.Trim().ToLower()) ||
                       jobSkill.Trim().ToLower().Contains(skill.Trim().ToLower()))
                        matchCount++;
                }
            }
            return matchCount;
        }
        // Calculate skill match percentage between candidate and job post
        public int Calculate(Candidate candidate,JobPost jobPost)
        {
            Converter _converter = new Converter();
            HashSet<string> candidateSkillSet = _converter.SkillSet(candidate.Skills);
            HashSet<string> jobSkillSet = _converter.SkillSet(jobPost.SkillsRequired);
            int matchCount =  this.SkillCountCalculate(candidateSkillSet, jobSkillSet);

            if (candidate.ExperienceLevel >= jobPost.ExperienceLevel)
                matchCount++;
            if (jobPost.Title.Trim().ToLower().Contains(candidate.CandidateType.Trim().ToLower()))
                matchCount++;
            return (int)Math.Ceiling((double)matchCount / (jobSkillSet.Count() + 2)*100);
        }

        // Calculate skill match percentage for filtering job posts
        public int Calculate(JobPostSearchDTO post, string? title, HashSet<string> candidateSkillSet,
            int minExperience,string? location,DateTime? postedAfter)
        {
            Converter _converter = new Converter();
            HashSet<string> jobSkillSet = _converter.SkillSet(post.SkillsRequired);
            int matchCount = this.SkillCountCalculate(candidateSkillSet,jobSkillSet);
            int totalMatchItems = jobSkillSet.Count();
            if (candidateSkillSet.Count == 0) totalMatchItems = 0;
            if (post.ExperienceLevel >= minExperience)
            {
                matchCount++; 
                totalMatchItems++;
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                if(post.Title.Trim().ToLower().Contains(title.Trim().ToLower()))
                    matchCount++;
                totalMatchItems++;
            }
            if(!string.IsNullOrWhiteSpace(location))
            {
                if (post.Location.Trim().ToLower().Contains(location.Trim().ToLower()))
                    matchCount++;
                totalMatchItems++;
            }
            if(postedAfter.HasValue)
            {
                if (post.PostedAt >= postedAfter.Value)
                        matchCount++;
                totalMatchItems++;
            }
            if(totalMatchItems == 0) return 100;
            int val =(int)Math.Ceiling((double)matchCount / totalMatchItems * 100);
            Console.WriteLine($"MatchCount: {matchCount}, TotalMatchItems: {totalMatchItems}, Value: {val}");
            return val;
        }
    }
}
