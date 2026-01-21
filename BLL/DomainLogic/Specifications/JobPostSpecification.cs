using BLL.DomainLogic.Calculators;
using BLL.DomainLogic.Converters;
using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainLogic.Specifications
{
    public class JobPostSpecification
    {
        private readonly ISkillMatchCalculator _skillMatch;
        public JobPostSpecification(ISkillMatchCalculator skillMatch) {
             _skillMatch = skillMatch;
        }
        public List<JobPostSearchDTO> Filter(List<JobPostSearchDTO> posts,string? title,
            string[]? skills, int minExperience, string? location, DateTime? posterAfter)
        {
            List<JobPostSearchDTO> filteredPosts = new List<JobPostSearchDTO>();
            Converter _converter = new Converter();
            HashSet<string> skillSet = _converter.SkillSet(_converter.SkillString(skills));

            foreach (var item in posts)
             if(_skillMatch.Calculate(item,title, skillSet, 
                 minExperience,location,posterAfter) >= 50)
                    filteredPosts.Add(item);
            return filteredPosts;
        }
    }
}
