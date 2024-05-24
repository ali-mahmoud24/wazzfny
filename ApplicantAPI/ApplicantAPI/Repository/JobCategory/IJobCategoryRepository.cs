using ApplicantAPI.Models;
using ApplicantAPI.Dtos.JobCategory;

namespace ApplicantAPI.Repository;

public interface IJobCategoryRepository
{
    Task<Response<IEnumerable<GetJobCayegoryDto>>> GetAllJobCategories();
    Task<Response<GetJobCayegoryDto>> AddJobCategory(JobCategory newJobCategory);
    Task<Response<int>> GetJobCategoriesCount();
}
