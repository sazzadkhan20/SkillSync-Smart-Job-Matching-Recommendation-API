using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;

namespace BLL.Helpers
{
    public class MapperConfig
    {
        static MapperConfiguration cfg = new MapperConfiguration(c => {
            c.CreateMap<Candidate, CandidateDTO>().ReverseMap();
            c.CreateMap<JobPost, JobPostDTO>().ReverseMap();
            c.CreateMap<JobApplication, JobApplicationDTO>().ReverseMap();
            c.CreateMap<JobPost, JobPostSearchDTO>().ReverseMap();
            c.CreateMap<JobPost, JobSpecificApplicationDetailsDTO>().ReverseMap();
            c.CreateMap<JobApplication, JobApplicationDetailsDTO>().ReverseMap().
                         ForMember(
                         dto => dto.Candidate,
                         src => src.MapFrom(c => c.Candidate)).
                         ForMember(
                         dto => dto.JobPost,
                         src => src.MapFrom(j => j.JobPost));

            //c.CreateMap<JobPost,JobPostWithApplicationDTO>().ReverseMap().
            //             ForMember(
            //             dto => dto.JobApplications,
            //             src => src.MapFrom(j => j.JobApplications));
            //c.CreateMap<Department, DepartmentCountDTO>().ForMember(
            //        dto => dto.Count,
            //        src => src.MapFrom(d => d.Students.Count)
            //);

        });
        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }

        public static JobPostWithApplicationDTO mapper(List<JobApplication> application, DateTime postedAt)
        {
            JobPostWithApplicationDTO jobPosts = new JobPostWithApplicationDTO()
            {
                CompanyName = application[0].JobPost.CompanyName,
                Title = application[0].JobPost.Title,
                SkillsRequired = application[0].JobPost.SkillsRequired,
                ExperienceLevel = application[0].JobPost.ExperienceLevel,
                Location = application[0].JobPost.Location,
                PostedAt = postedAt,
                JobApplications = application.Select(app => new JobSpecificApplicationDetailsDTO
                {
                    ApplicationStatus = app.ApplicationStatus,
                    SkillMatchPercent = app.SkillMatchPercent,
                    AppliedDate = app.AppliedDate,
                    Candidate = new CandidateDTO
                    {
                        FullName = app.Candidate.FullName,
                        Email = app.Candidate.Email,
                        Phone = app.Candidate.Phone,
                        Skills = app.Candidate.Skills,
                        CandidateType = app.Candidate.CandidateType,
                        ExperienceLevel = app.Candidate.ExperienceLevel

                    }
                }).ToList()


            };
            return jobPosts;
        }
    }
}
