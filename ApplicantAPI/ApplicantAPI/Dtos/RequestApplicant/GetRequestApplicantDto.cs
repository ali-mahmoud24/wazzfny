using ApplicantAPI.Dtos.ComputerSkill;
using ApplicantAPI.Dtos.Language;

namespace ApplicantAPI.Dtos.RequestApplicant;

public class GetRequestApplicantDto
{
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public DateTime PostDate { get; set; }

    public DateTime StartPublish { get; set; }

    public DateTime EndPublish { get; set; }

    public byte? AgeFrom { get; set; }

    public byte? AgeTo { get; set; }

    public byte? Gender { get; set; }

    public double? SalaryFrom { get; set; }

    public double? SalaryTo { get; set; }

    public bool? IsNegotiate { get; set; }

    public bool? WorkType { get; set; }

    public byte? WorkHour { get; set; }

    public string? Details { get; set; }

    public bool IsEnd { get; set; }


    public byte ExperienceId { get; set; }
    public string ExperienceName { get; set; } = string.Empty;


    public int JobId { get; set; }
    public string JobName { get; set; } = string.Empty;

    public int JobCategoryId { get; set; }
    public string JobCategoryName { get; set; } = string.Empty;




    public IEnumerable<GetLanguageDto?> Languages { get; set; } = Enumerable.Empty<GetLanguageDto>();
    public IEnumerable<GetComputerSkillDto?> ComputerSkills { get; set; } = Enumerable.Empty<GetComputerSkillDto>();
}
