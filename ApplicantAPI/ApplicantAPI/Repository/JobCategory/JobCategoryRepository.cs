using Microsoft.EntityFrameworkCore;
using AutoMapper;

using ApplicantAPI.Data;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.JobCategory;


namespace ApplicantAPI.Repository;

public class JobCategoryRepository : IJobCategoryRepository
{
    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public JobCategoryRepository(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Response<IEnumerable<GetJobCayegoryDto>>> GetAllJobCategories()
    {

        var response = new Response<IEnumerable<GetJobCayegoryDto>>();

        try
        {

            var loadedJobCategories = await _dbContext.JobCategories.ToListAsync();

            if (loadedJobCategories == null || !loadedJobCategories.Any())
            {
                response.Success = false;
                response.Message = "No Job Categories Found.";

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<GetJobCayegoryDto>>(loadedJobCategories); ;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }



    public async Task<Response<GetJobCayegoryDto>> AddJobCategory(JobCategory newJobCategory)
    {

        var response = new Response<GetJobCayegoryDto>();

        try
        {
            await _dbContext.JobCategories.AddAsync(newJobCategory);
            await _dbContext.SaveChangesAsync();


            response.Data = _mapper.Map<GetJobCayegoryDto>(newJobCategory);
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }



    public async Task<Response<int>> GetJobCategoriesCount()
    {

        var response = new Response<int>();

        try
        {

            var count = await _dbContext.JobCategories.CountAsync();

            response.Data = count;
            return response;

        }
        catch (Exception ex)
        {
            response.Data = 0;
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


}
