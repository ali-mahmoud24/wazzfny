using Microsoft.EntityFrameworkCore;
using AutoMapper;

using ApplicantAPI.Data;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Experience;


namespace ApplicantAPI.Repository;

public class ExperienceRepository : IExperienceRepository
{

    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public ExperienceRepository(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Response<IEnumerable<GetExperienceDto>>> GetAllExperiences()
    {

        var response = new Response<IEnumerable<GetExperienceDto>>();

        try
        {

            var loadedExperiences = await _dbContext.Experiences.ToListAsync();

            if (loadedExperiences == null || !loadedExperiences.Any())
            {
                response.Success = false;
                response.Message = "No Experiences Found.";

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<GetExperienceDto>>(loadedExperiences); ;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }



    public async Task<Response<GetExperienceDto>> AddExperience(Experience newExperience)
    {

        var response = new Response<GetExperienceDto>();

        try
        {
            await _dbContext.Experiences.AddAsync(newExperience);
            await _dbContext.SaveChangesAsync();


            response.Data = _mapper.Map<GetExperienceDto>(newExperience);
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }


    public async Task<Response<int>> GetExperiencesCount()
    {

        var response = new Response<int>();

        try
        {

            var count = await _dbContext.Experiences.CountAsync();

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
