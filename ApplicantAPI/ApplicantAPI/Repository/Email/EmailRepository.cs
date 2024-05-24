using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

using ApplicantAPI.Dtos.Email;
using static System.Net.WebRequestMethods;

namespace ApplicantAPI.Repository;

public class EmailRepository : IEmailRepository
{
    private readonly IConfiguration _config;

    public EmailRepository(IConfiguration config)
    {
        _config = config;
    }


    public void SendResetPasswordEmail(SendResetPasswordEmail sendResetPasswordEmail)
    {
        var resetUrl = "http://localhost:4200/reset-password?resetToken=" + sendResetPasswordEmail.ResetToken;


        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailSettings:Username").Value));
        email.To.Add(MailboxAddress.Parse(sendResetPasswordEmail.To));
        email.Subject = "إعادة تعيين كلمة السر";
        //email.Body = new TextPart(TextFormat.Html)
        //{
        //    Text = $@"<h1>اضغط على الرابط أدناه لاعادة تعيين كلمة المرور</h1>
        //              <a href={resetUrl}>الرابط</a>"
        //};

        email.Body = new TextPart(TextFormat.Html)
        {
            Text = $@"
<!DOCTYPE html>
<html lang=""ar"" dir=""rtl"">
    <head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>استعادة كلمة السر</title>
    <style>
        /* CSS styles for email */
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333333;
        }}
        
        .container {{
            max-width: 600px;
            margin: 0 auto;
        }}
        
        .header {{
            background-color: #f8f8f8;
            padding: 20px;
            text-align: center;
        }}
        
        .logo {{
            width: 100px;
            height: auto;
        }}
        
        .content {{
            background-color: #ffffff;
            padding: 20px;
            border-radius: 4px;
            margin-top: 20px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }}
        
        .footer {{
            background-color: #f8f8f8;
            padding: 20px;
            text-align: center;
        }}
        
        .button {{
            display: inline-block;
            background-color: #007bff;
            color: #ffffff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 4px;
            margin-top: 20px;
        }}
        
        .button:hover {{
            background-color: #0056b3;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <img class=""logo"" src=""https://example.com/logo.png"" alt=""شعار"">
            <h1>إعادة تعيين كلمة السر</h1>
        </div>
        <div class=""content"">
            <p>مرحبًا،</p>
            <p>لإعادة تعيين كلمة السر الخاصة بك، يرجى النقر على الزر أدناه:</p>
            <a class=""button"" href={resetUrl}>إعادة تعيين كلمة السر</a>
            <p>إذا لم تكن قد طلبت إعادة تعيين كلمة السر، يمكنك تجاهل هذا البريد الإلكتروني.</p>
        </div>
        <div class=""footer"">
            <p>شكرًا لاستخدامكم خدمتنا!</p>
            <p>فريق الدعم الفني</p>
        </div>
    </div>
</body>
</html>"
        };




        try
        {

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailSettings:Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailSettings:Username").Value, _config.GetSection("EmailSettings:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }



    public void SendEmail(SendEmailDto sendEmailDto)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailSettings:Username").Value));
        email.To.Add(MailboxAddress.Parse(sendEmailDto.To));
        email.Subject = sendEmailDto.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = sendEmailDto.Body };


        try
        {

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailSettings:Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailSettings:Username").Value, _config.GetSection("EmailSettings:Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }
}