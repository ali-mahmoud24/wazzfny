using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ApplicantAPI.Repository;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Language;
using ApplicantAPI.Dtos.ComputerSkill;

namespace ApplicantAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ComputerSkillController : ControllerBase
{
    private readonly IComputerSkillRepository _computerSkillRepository;
    private readonly IMapper _mapper;


    public ComputerSkillController(IComputerSkillRepository computerSkillRepository, IMapper mapper)
    {
        _computerSkillRepository = computerSkillRepository;
        _mapper = mapper;
    }


    [HttpGet("GetComputerSkills")]
    public async Task<ActionResult<Response<IEnumerable<GetComputerSkillDto>>>> GetAllComputerSkills()
    {
        var response = await _computerSkillRepository.GetAllComputerSkills();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [HttpPost("AddComputerSkill")]
    public async Task<ActionResult<Response<GetComputerSkillDto>>> AddLanguage(AddComputerSkillDto addComputerSkillDto)
    {
        if (addComputerSkillDto == null)
        {
            return BadRequest();
        }

        var newLanguage = _mapper.Map<ComputerSkill>(addComputerSkillDto);

        var response = await _computerSkillRepository.AddComputerSkill(newLanguage);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpGet("GetComputerSkillsCount")]
    public async Task<ActionResult<Response<int>>> GetComputerSkillsCount()
    {
        var response = await _computerSkillRepository.GetComputerSkillsCount();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}