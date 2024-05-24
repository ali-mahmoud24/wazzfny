namespace ApplicantAPI.Dtos.RequestApplicant;

public class EditRequestApplicantDto
{
    public int RequestId { get; set; }

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

    public byte ExperienceId { get; set; }
    public int JobId { get; set; }


    public ICollection<int> LanguagesIds { get; set; } = new List<int>();

    public ICollection<int> ComputerSkillsIds { get; set; } = new List<int>();
}
