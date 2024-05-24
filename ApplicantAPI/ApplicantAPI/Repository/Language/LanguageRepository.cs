using ApplicantAPI.Data;
using ApplicantAPI.Dtos.Language;
using ApplicantAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApplicantAPI.Repository;

public class LanguageRepository : ILanguageRepository
{
    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public LanguageRepository(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Response<IEnumerable<GetLanguageDto>>> GetAllLanguages()
    {

        var response = new Response<IEnumerable<GetLanguageDto>>();

        try
        {

            var loadedLanguages = await _dbContext.Languages.ToListAsync();

            if (loadedLanguages == null || !loadedLanguages.Any())
            {
                response.Success = false;
                response.Message = "No Languages Found";

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<GetLanguageDto>>(loadedLanguages); ;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }



    public async Task<Response<GetLanguageDto>> AddLanguage(Language newLanguage)
    {

        var response = new Response<GetLanguageDto>();

        try
        {
            await _dbContext.Languages.AddAsync(newLanguage);
            await _dbContext.SaveChangesAsync();


            response.Data = _mapper.Map<GetLanguageDto>(newLanguage);
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<Response<int>> GetLanguagesCount()
    {

        var response = new Response<int>();

        try
        {

            var count = await _dbContext.Languages.CountAsync();

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
