namespace ApplicantAPI.Dtos.Email;

public class SendResetPasswordEmail
{
    public string To { get; set; } = string.Empty;
    public string ResetToken { get; set; } = string.Empty;

    //public string Subject { get; set; } = string.Empty;
    //public string Body { get; set; } = string.Empty;
}
