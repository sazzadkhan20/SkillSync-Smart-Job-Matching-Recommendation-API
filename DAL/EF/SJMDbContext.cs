using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class SJMDbContext: DbContext
    {
        public SJMDbContext(DbContextOptions<SJMDbContext> opt) : base(opt)
        {

        }


        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
