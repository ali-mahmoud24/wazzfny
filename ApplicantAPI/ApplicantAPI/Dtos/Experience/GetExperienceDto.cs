namespace ApplicantAPI.Dtos.Experience;

public class GetExperienceDto
{
    public int ExperienceId { get; set; }

    public string ExperienceName { get; set; } = string.Empty;

    public string? Notes { get; set; }

    //public virtual ICollection<RequestApplicant> RequestApplicants { get; set; } = new List<RequestApplicant>();
}
