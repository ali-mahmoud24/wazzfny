using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class Job
{
    public int JobId { get; set; }

    public int JobCategoryId { get; set; }

    public string JobName { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual JobCategory JobCategory { get; set; } = null!;

    public virtual ICollection<RequestApplicant> RequestApplicants { get; set; } = new List<RequestApplicant>();
}
