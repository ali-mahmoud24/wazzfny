using AutoMapper;

using ApplicantAPI.Models;
using ApplicantAPI.Dtos.ComputerSkill;
using ApplicantAPI.Dtos.Experience;
using ApplicantAPI.Dtos.Language;
using ApplicantAPI.Dtos.JobCategory;
using ApplicantAPI.Dtos.Job;
using ApplicantAPI.Dtos.RequestApplicant;

namespace ApplicantAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // LANGUAGE
        CreateMap<AddLanguageDto, Language>();
        CreateMap<Language, GetLanguageDto>();

        // COMPUTER SKILL
        CreateMap<AddComputerSkillDto, ComputerSkill>();
        CreateMap<ComputerSkill, GetComputerSkillDto>();

        // EXPERIENCE
        CreateMap<AddExperienceDto, Experience>();
        CreateMap<Experience, GetExperienceDto>();

        // JOB CATEGORY
        CreateMap<AddJobCategoryDto, JobCategory>();
        CreateMap<JobCategory, GetJobCayegoryDto>();

        // JOB
        CreateMap<AddJobDto, Job>();
        CreateMap<Job, GetJobDto>();

        // REQUEST APPLICANT
        CreateMap<AddRequestApplicantDto, RequestApplicant>();
        CreateMap<RequestApplicant, GetRequestApplicantDto>();
        // EDIT
        CreateMap<EditRequestApplicantDto, RequestApplicant>()
                        .IgnoreAllPropertiesWithAnInaccessibleSetter()
                        .ForPath(s => s.PostDate, opt => opt.Ignore())
                        .ForPath(s => s.IsEnd, opt => opt.Ignore())
                        .ForPath(s => s.RequestId, opt => opt.Ignore());
    }
}