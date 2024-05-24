namespace ApplicantAPI.Dtos.JobCategory;

public class AddJobCategoryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
