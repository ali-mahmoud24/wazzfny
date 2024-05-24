using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Experience;

namespace ApplicantAPI.Repository;

public interface IExperienceRepository
{
    Task<Response<IEnumerable<GetExperienceDto>>> GetAllExperiences();
    Task<Response<GetExperienceDto>> AddExperience(Experience newExperience);

    Task<Response<int>> GetExperiencesCount();
}