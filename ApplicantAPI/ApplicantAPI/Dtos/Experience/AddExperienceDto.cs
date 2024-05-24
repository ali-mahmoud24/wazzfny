namespace ApplicantAPI.Dtos.Experience;

public class AddExperienceDto
{
    public string ExperienceName { get; set; } = string.Empty;

    public string? Notes { get; set; }
}