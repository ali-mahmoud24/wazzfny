namespace ApplicantAPI.Dtos.Auth;

public class ResetPasswordDto
{
    public string ResetToken { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
