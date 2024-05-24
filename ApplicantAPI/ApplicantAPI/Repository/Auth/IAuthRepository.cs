using ApplicantAPI.Dtos.Auth;
using ApplicantAPI.Models;

namespace ApplicantAPI.Repository;

public interface IAuthRepository
{
    Task<Response<GetLoginDataDto>> Login(LoginDto loginDto);
    Task<Response<GetLoginDataDto>> Register(RegisterDto registerDto);

    Task<Response<bool>> ForgotPassword(ForgotPasswordDto forgotPasswordDto);

    Task<Response<bool>> ResetPassword(ResetPasswordDto resetPasswordDto);


    Task<bool> UserExists(string email);
}
