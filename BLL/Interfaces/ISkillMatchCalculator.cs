using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISkillMatchCalculator
    {
        public int Calculate(Candidate candidate, JobPost jobPost);
    }
}
