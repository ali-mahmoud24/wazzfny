using ApplicantAPI.Data;
using ApplicantAPI.Dtos.ComputerSkill;
using ApplicantAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApplicantAPI.Repository;

public class ComputerSkillRepository : IComputerSkillRepository
{

    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;

    public ComputerSkillRepository(DataContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Response<IEnumerable<GetComputerSkillDto>>> GetAllComputerSkills()
    {

        var response = new Response<IEnumerable<GetComputerSkillDto>>();

        try
        {

            var loadedComputerSkills = await _dbContext.ComputerSkills.ToListAsync();

            if (loadedComputerSkills == null || !loadedComputerSkills.Any())
            {
                response.Success = false;
                response.Message = "No Computer Skills Found";

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<GetComputerSkillDto>>(loadedComputerSkills); ;

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }



    public async Task<Response<GetComputerSkillDto>> AddComputerSkill(ComputerSkill newComputerSkill)
    {

        var response = new Response<GetComputerSkillDto>();

        try
        {
            await _dbContext.ComputerSkills.AddAsync(newComputerSkill);
            await _dbContext.SaveChangesAsync();


            response.Data = _mapper.Map<GetComputerSkillDto>(newComputerSkill);
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }




    public async Task<Response<int>> GetComputerSkillsCount()
    {

        var response = new Response<int>();

        try
        {

            var count = await _dbContext.ComputerSkills.CountAsync();

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
