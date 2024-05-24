using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ApplicantAPI.Repository;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Language;


namespace ApplicantAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LanguageController : ControllerBase
{
    private readonly ILanguageRepository _langaugeRepository;
    private readonly IMapper _mapper;


    public LanguageController(ILanguageRepository languageRepository, IMapper mapper)
    {
        _langaugeRepository = languageRepository;
        _mapper = mapper;
    }


    [HttpGet("GetLanguages")]
    public async Task<ActionResult<Response<IEnumerable<GetLanguageDto>>>> GetAllLanguages()
    {
        var response = await _langaugeRepository.GetAllLanguages();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [HttpPost("AddLanguage")]
    public async Task<ActionResult<Response<GetLanguageDto>>> AddLanguage(AddLanguageDto addLanguageDto)
    {
        if (addLanguageDto == null)
        {
            return BadRequest();
        }

        var newLanguage = _mapper.Map<Language>(addLanguageDto);

        var response = await _langaugeRepository.AddLanguage(newLanguage);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }


    [HttpGet("GetLanguagesCount")]
    public async Task<ActionResult<Response<int>>> GetLanguagesCount()
    {
        var response = await _langaugeRepository.GetLanguagesCount();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}