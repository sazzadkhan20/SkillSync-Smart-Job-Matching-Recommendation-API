using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainLogic.Converters
{
    public class Converter
    {
        // Remove Duplicate
        public HashSet<string> SkillSet(string skillSet)
        {
            if (string.IsNullOrWhiteSpace(skillSet))
                return new HashSet<string>();
            return new HashSet<string>(skillSet.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                StringComparer.OrdinalIgnoreCase);
        }

        public string SkillString(string[]? skills)
        {
            string convertedSkills = string.Empty;
            if (skills != null && skills.Length > 0)
            {
                foreach (var item in skills)
                    convertedSkills += item + ",";
            }
            return convertedSkills.TrimEnd(',');
        }

        // Sort the result based on PostedAt
        public List<JobPostSearchDTO> sortByDate(List<JobPostSearchDTO> result,string sort)
        {
            if (sort.Equals("asc"))
                result = result.OrderBy(post => post.PostedAt).ToList();
            else
                result = result.OrderByDescending(post => post.PostedAt).ToList();
            return result;
        }
    }
}
