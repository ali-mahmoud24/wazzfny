using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ApplicantAPI.Repository;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Experience;

namespace ApplicantAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IMapper _mapper;


    public ExperienceController(IExperienceRepository computerSkillRepository, IMapper mapper)
    {
        _experienceRepository = computerSkillRepository;
        _mapper = mapper;
    }


    [HttpGet("GetExperiences")]
    public async Task<ActionResult<Response<IEnumerable<GetExperienceDto>>>> GetAllExperiences()
    {
        var response = await _experienceRepository.GetAllExperiences();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [HttpPost("AddExperience")]
    public async Task<ActionResult<Response<GetExperienceDto>>> AddExperience(AddExperienceDto addExperienceDto)
    {
        if (addExperienceDto == null)
        {
            return BadRequest();
        }

        var newExperience = _mapper.Map<Experience>(addExperienceDto);

        var response = await _experienceRepository.AddExperience(newExperience);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("GetExperiencesCount")]
    public async Task<ActionResult<Response<int>>> GetExperiencesCount()
    {
        var response = await _experienceRepository.GetExperiencesCount();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}