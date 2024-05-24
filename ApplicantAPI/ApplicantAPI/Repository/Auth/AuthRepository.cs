using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicantAPI.Data;
using ApplicantAPI.Models;
using ApplicantAPI.Dtos.Auth;
using ApplicantAPI.Dtos.Email;
using System.Text;

namespace ApplicantAPI.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _dbContext;
    private readonly IEmailRepository _emailRepository;

    private readonly IConfiguration _configuration;

    public AuthRepository(DataContext dbContext, IEmailRepository emailRepository, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _emailRepository = emailRepository;
        _configuration = configuration;
    }

    public async Task<Response<GetLoginDataDto>> Login(LoginDto loginDto)
    {
        var response = new Response<GetLoginDataDto>();
        var loadedUser = await _dbContext.Users
                                   .FirstOrDefaultAsync(u => u.Email.Equals(loginDto.Email));

        if (loadedUser == null)
        {
            response.Success = false;
            response.Message = "WRONG_CREDENTIALS";
            //response.Message = "User not found.";
        }
        else if (!VerifyPasswordHash(loginDto.Password, loadedUser.PasswordHash, loadedUser.PasswordSalt))
        {
            response.Success = false;
            response.Message = "WRONG_CREDENTIALS";
        }
        else
        {

            // Number of seconds in a day = 86400
            int expiresIn = 24 * 60 * 60;

            var loadedUserData = new GetLoginDataDto
            {
                UserId = loadedUser.UserId,
                Email = loadedUser.Email,
                Role = loadedUser.Role,
                Token = CreateToken(loadedUser),
                ExpiresIn = expiresIn,
            };

            response.Data = loadedUserData;
        }

        return response;
    }

    public async Task<Response<GetLoginDataDto>> Register(RegisterDto registerDto)
    {
        var response = new Response<GetLoginDataDto>();

        var newUser = new User
        {
            Email = registerDto.Email,
            Role = registerDto.Role
        };

        if (await UserExists(newUser.Email))
        {
            response.Success = false;
            response.Message = "EMAIL_EXISTS";
            return response;
        }

        CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;

        try
        {
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            // Number of seconds in a day = 86400
            int expiresIn = 24 * 60 * 60;

            var newUserData = new GetLoginDataDto
            {
                UserId = newUser.UserId,
                Email = newUser.Email,
                Role = newUser.Role,
                Token = CreateToken(newUser),
                ExpiresIn = expiresIn,
            };


            response.Data = newUserData;

            response.Message = "Added New User Succesfully.";
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }


    public async Task<Response<bool>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var response = new Response<bool>();
        try
        {
            var user = await GetUserEntityById(forgotPasswordDto.Email);
            if (user == null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "No User Found With This Email.";

                return response;
            }

            var resetToken = CreateToken(user);

            var emailToSend = new SendResetPasswordEmail { To = forgotPasswordDto.Email, ResetToken = resetToken };
            _emailRepository.SendResetPasswordEmail(emailToSend);



            response.Data = true;

            response.Message = "Email Sent Succesfully.";
            return response;
        }
        catch (Exception ex)
        {
            response.Data = false;
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }



    public async Task<Response<bool>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var response = new Response<bool>();


        try
        {

            // Retrieve user from the database using the reset token
            var user = await this.GetUserByResetToken(resetPasswordDto.ResetToken);

            if (user == null)
            {
                response.Data = false;
                response.Success = false;

                return response;
            }

            // Update the user's password

            CreatePasswordHash(resetPasswordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dbContext.SaveChangesAsync();

            response.Data = true;
            response.Message = "Changed Password Succesfully";

            return response;
        }
        catch (Exception ex)
        {
            response.Data = false;
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }

    }


    public async Task<bool> UserExists(string email)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == email))
        {
            return true;
        }
        return false;
    }

    private async Task<User?> GetUserEntityById(string email)
    {
        var user = await _dbContext.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
        if (user == null)
        {
            return null;
        }
        return user;
    }



    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };


        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }









    private async Task<User> GetUserByResetToken(string token)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal claimsPrincipal;

        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            string tokenUserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _dbContext.Users.Where(u => u.UserId == Int32.Parse(tokenUserId)).SingleOrDefaultAsync();

            return user;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }















    private bool VerifyToken(string token, int userId, string email, string role)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal claimsPrincipal;

        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        }
        catch
        {
            return false; // Token validation failed
        }

        // Verify user ID and email
        string tokenUserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        string tokenEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        string tokenRole = claimsPrincipal.FindFirstValue(ClaimTypes.Role);


        if (tokenUserId != userId.ToString() || tokenEmail != email || tokenRole != role)
        {
            return false; // Token does not belong to the specified user
        }

        return true; // Token is valid and belongs to the specified user
    }




}
