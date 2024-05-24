namespace ApplicantAPI.Dtos.Job;

public class AddJobDto
{
    public int JobCategoryId { get; set; }

    public string JobName { get; set; } = string.Empty;

    public string? Notes { get; set; }
}
