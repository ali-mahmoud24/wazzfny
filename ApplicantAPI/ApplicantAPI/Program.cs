using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using ApplicantAPI.Data;
using ApplicantAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//  Database Connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//  Ignore JSON Cycles in case of returning Nested Models
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.AddAutoMapper(typeof(Program).Assembly);

//  Configure Dependency Injection for repositories

builder.Services.AddScoped<IAuthRepository, AuthRepository>();


builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IComputerSkillRepository, ComputerSkillRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();

builder.Services.AddScoped<IJobRepository, JobRepository>();

builder.Services.AddScoped<IRequestApplicantRepository, RequestApplicantRepository>();

builder.Services.AddScoped<IEmailRepository, EmailRepository>();






//  Configure JWT Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


//  Configure UI For adding token in Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.\r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});




// Configure Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", (corsBuilder) =>
    {
        // Angular - React - Vue
        corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:5000", "http://localhost:8000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
    });

    options.AddPolicy("ProdCors", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("https://myProductionSite.com")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();