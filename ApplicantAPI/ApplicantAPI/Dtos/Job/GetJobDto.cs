using ApplicantAPI.Models;

namespace ApplicantAPI.Dtos.Job;

public class GetJobDto
{
    public int JobId { get; set; }

    public int JobCategoryId { get; set; }

    public string JobName { get; set; } = string.Empty;

    public string? Notes { get; set; }

    //public virtual JobCategory JobCategory { get; set; } = null!;

    //public virtual ICollection<RequestApplicant> RequestApplicants { get; set; } = new List<RequestApplicant>();
}
