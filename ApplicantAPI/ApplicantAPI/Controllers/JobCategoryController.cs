using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ApplicantAPI.Models;
using ApplicantAPI.Repository;
using ApplicantAPI.Dtos.JobCategory;

namespace ApplicantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobCategoryController : ControllerBase
{
    private readonly IJobCategoryRepository _jobCategoryRepository;
    private readonly IMapper _mapper;

    public JobCategoryController(IJobCategoryRepository jobCategoryRepository, IMapper mapper)
    {
        _jobCategoryRepository = jobCategoryRepository;
        _mapper = mapper;
    }


    [HttpGet("GetJobCategories")]
    public async Task<ActionResult<Response<IEnumerable<GetJobCayegoryDto>>>> GetAllJobCategories()
    {
        var response = await _jobCategoryRepository.GetAllJobCategories();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [HttpPost("AddJobCategory")]
    public async Task<ActionResult<Response<GetJobCayegoryDto>>> AddLanguage(AddJobCategoryDto addJobCategoryDto)
    {
        if (addJobCategoryDto == null)
        {
            return BadRequest();
        }

        var newJobCategory = _mapper.Map<JobCategory>(addJobCategoryDto);

        var response = await _jobCategoryRepository.AddJobCategory(newJobCategory);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }


    [HttpGet("GetJobCategoriesCount")]
    public async Task<ActionResult<Response<int>>> GetJobCategoriesCount()
    {
        var response = await _jobCategoryRepository.GetJobCategoriesCount();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


}