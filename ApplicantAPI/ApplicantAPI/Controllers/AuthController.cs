using Microsoft.AspNetCore.Mvc;
using ApplicantAPI.Repository;
using ApplicantAPI.Dtos.Auth;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Email;
using Microsoft.EntityFrameworkCore;

namespace ApplicantAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    private readonly IEmailRepository _emailRepository;


    public AuthController(IAuthRepository authRepository, IEmailRepository emailRepository)
    {
        _authRepository = authRepository;
        _emailRepository = emailRepository;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<Response<GetLoginDataDto>>> Register(RegisterDto request)
    {
        var response = await _authRepository.Register(request);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<Response<GetLoginDataDto>>> Login(LoginDto request)
    {
        var response = await _authRepository.Login(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }


    [HttpPost("sendemail")]
    public ActionResult SendEmail(SendEmailDto request)
    {
        _emailRepository.SendEmail(request);
        return Ok();
    }



    [HttpPost("ForgotPassword")]
    public async Task<ActionResult<Response<bool>>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var response = await _authRepository.ForgotPassword(forgotPasswordDto);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);

    }


    [HttpPost("ResetPassword")]
    public async Task<ActionResult<Response<bool>>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var response = await _authRepository.ResetPassword(resetPasswordDto);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

}