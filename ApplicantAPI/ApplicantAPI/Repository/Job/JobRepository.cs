using Microsoft.EntityFrameworkCore;
using AutoMapper;

using ApplicantAPI.Models;
using ApplicantAPI.Data;
using ApplicantAPI.Dtos.Job;

namespace ApplicantAPI.Repository;

public class JobRepository : IJobRepository
{
    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public JobRepository(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Response<IEnumerable<GetJobDto>>> GetAllJobs()
    {

        var response = new Response<IEnumerable<GetJobDto>>();

        try
        {

            var loadedJobs = await _dbContext.Jobs.ToListAsync();

            if (loadedJobs == null || !loadedJobs.Any())
            {
                response.Success = false;
                response.Message = "No Jobs Found.";

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<GetJobDto>>(loadedJobs); ;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    public async Task<Response<IEnumerable<GetJobDto>>> GetJobsByCategoryId(int jobCategoryId)
    {

        var response = new Response<IEnumerable<GetJobDto>>();

        try
        {

            var loadedJobs = await _dbContext.Jobs
                                             .Where(j => j.JobCategoryId == jobCategoryId)
                                             .ToListAsync();

            if (loadedJobs == null || !loadedJobs.Any())
            {
                response.Success = false;
                response.Message = "No Jobs Found in this category.";

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<GetJobDto>>(loadedJobs); ;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }





    public async Task<Response<GetJobDto>> AddJob(Job newJob)
    {

        var response = new Response<GetJobDto>();

        try
        {
            await _dbContext.Jobs.AddAsync(newJob);
            await _dbContext.SaveChangesAsync();


            response.Data = _mapper.Map<GetJobDto>(newJob);
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
}
