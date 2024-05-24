using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Job;

namespace ApplicantAPI.Repository;

public interface IJobRepository
{
    Task<Response<IEnumerable<GetJobDto>>> GetAllJobs();

    Task<Response<IEnumerable<GetJobDto>>> GetJobsByCategoryId(int jobCategoryId);

    Task<Response<GetJobDto>> AddJob(Job newJob);
}
