using ApplicantAPI.Models;

namespace ApplicantAPI.Dtos.JobCategory;

public class GetJobCayegoryDto
{
    public int JobCategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Notes { get; set; }

    //public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
