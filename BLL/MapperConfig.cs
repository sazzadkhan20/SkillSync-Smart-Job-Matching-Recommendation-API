using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration cfg = new MapperConfiguration(c => {
            c.CreateMap<Candidate, CandidateDTO>().ReverseMap();
            c.CreateMap<JobPost, JobPostDTO>().ReverseMap();
            c.CreateMap<JobApplication, JobApplicationDTO>().ReverseMap();
            c.CreateMap<JobApplication, JobApplicationDetailsDTO>().ReverseMap().
                         ForMember(
                         dto => dto.Candidate,
                         src => src.MapFrom(c => c.Candidate)).
                         ForMember(
                         dto => dto.JobPost,
                         src => src.MapFrom(j => j.JobPost));
            //c.CreateMap<Student, StudentDTO>().ReverseMap();
            //c.CreateMap<Student, StudentDTO>().ReverseMap();
            //c.CreateMap<Department, DepartmentStudentDTO>().ReverseMap();
            //c.CreateMap<Department, DepartmentCountDTO>().ForMember(
            //        dto => dto.Count,
            //        src => src.MapFrom(d => d.Students.Count)
            //);
            //c.CreateMap<Department, DepartmentDTO>().ReverseMap();
            //c.CreateMap<Department, DepartmentDTO>().ReverseMap();

        });
        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
