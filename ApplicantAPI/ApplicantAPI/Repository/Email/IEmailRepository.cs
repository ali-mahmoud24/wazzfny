using ApplicantAPI.Dtos.Email;

namespace ApplicantAPI.Repository;

public interface IEmailRepository
{
    void SendEmail(SendEmailDto sendEmailDto);

    void SendResetPasswordEmail(SendResetPasswordEmail sendResetPasswordEmail);

}