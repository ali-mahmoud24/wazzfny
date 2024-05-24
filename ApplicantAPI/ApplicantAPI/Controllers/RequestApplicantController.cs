using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ApplicantAPI.Repository;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.RequestApplicant;
using Microsoft.AspNetCore.Authorization;

namespace ApplicantAPI.Controllers;


[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RequestApplicantController : ControllerBase
{
    private readonly IRequestApplicantRepository _requestApplicantRepostiry;
    private readonly IMapper _mapper;


    public RequestApplicantController(IRequestApplicantRepository requestApplicantRepository, IMapper mapper)
    {
        _requestApplicantRepostiry = requestApplicantRepository;
        _mapper = mapper;
    }


    [HttpGet("Admin/GetRequestApplicants")]
    public async Task<ActionResult<Response<IEnumerable<GetRequestApplicantDto>>>> GetAdminRequestApplicants()
    {
        var response = await _requestApplicantRepostiry.GetAdminRequestApplicants();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [AllowAnonymous]
    [HttpGet("GetRequestApplicants")]
    public async Task<ActionResult<Response<IEnumerable<GetRequestApplicantDto>>>> GetAllRequestApplicants()
    {
        var response = await _requestApplicantRepostiry.GetAllRequestApplicants();

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [Authorize (Roles ="Owner")]
    [HttpGet("GetRequestApplicants/{userId}")]
    public async Task<ActionResult<Response<IEnumerable<GetRequestApplicantDto>>>> GetRequestApplicantsByOwnerId(int userId)
    {
        var response = await _requestApplicantRepostiry.GetRequestApplicantsByOwnerId(userId);

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }


    [Authorize(Roles = "Owner")]
    [HttpPost("AddRequestApplicant")]
    public async Task<ActionResult<Response<RequestApplicant>>> AddRequestApplicant(AddRequestApplicantDto addRequestApplicantDto)
    {
        if (addRequestApplicantDto == null)
        {
            return BadRequest();
        }

        var response = await _requestApplicantRepostiry.AddRequestApplicant(addRequestApplicantDto);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }


    [Authorize(Roles = "Owner")]
    [HttpPatch("EditRequestApplicant")]
    public async Task<ActionResult<Response<RequestApplicant>>> EditRequestApplicant(EditRequestApplicantDto editRequestApplicantDto)
    {
        if (editRequestApplicantDto == null)
        {
            return BadRequest();
        }

        var response = await _requestApplicantRepostiry.EditRequestApplicant(editRequestApplicantDto);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }


    [Authorize(Roles = "Owner")]
    [HttpPatch("EditIsEndRequestApplicant")]
    public async Task<ActionResult<Response<bool>>> EditIsEndRequestApplicant(EditIsEndRequestApplicantDto editIsEndRequestApplicantDto)
    {
        if (editIsEndRequestApplicantDto == null)
        {
            return BadRequest();
        }

        var response = await _requestApplicantRepostiry.EditIsEndRequestApplicant(editIsEndRequestApplicantDto);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [Authorize(Roles = "Owner")]
    [HttpDelete("DeleteRequestApplicant/{requestId}")]
    public async Task<ActionResult<Response<bool>>> DeleteRequestApplicantById(int requestId)
    {
        var response = await _requestApplicantRepostiry.DeleteRequestApplicantById(requestId);

        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}
