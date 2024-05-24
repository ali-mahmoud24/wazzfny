using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ApplicantAPI.Models;
using ApplicantAPI.Repository;
using ApplicantAPI.Dtos.Job;

namespace ApplicantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobRepository _jobRepository;
    private readonly IMapper _mapper;


    public JobController(IJobRepository jobRepository, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _mapper = mapper;
    }


    [HttpGet("GetJobs")]
    public async Task<ActionResult<Response<IEnumerable<GetJobDto>>>> GetAllJobs()
    {
        var response = await _jobRepository.GetAllJobs();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet("GetJobsByCategory/{jobCategoryId}")]
    public async Task<ActionResult<Response<IEnumerable<GetJobDto>>>> GetJobsByCategoryId(int jobCategoryId)
    {
        var response = await _jobRepository.GetJobsByCategoryId(jobCategoryId);

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [HttpPost("AddJob")]
    public async Task<ActionResult<Response<GetJobDto>>> AddJob(AddJobDto addJobDto)
    {
        if (addJobDto == null)
        {
            return BadRequest();
        }

        var newJob = _mapper.Map<Job>(addJobDto);

        var response = await _jobRepository.AddJob(newJob);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

}
