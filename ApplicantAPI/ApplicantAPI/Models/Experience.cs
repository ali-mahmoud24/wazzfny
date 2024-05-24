using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class Experience
{
    public byte ExperienceId { get; set; }

    public string ExperienceName { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<RequestApplicant> RequestApplicants { get; set; } = new List<RequestApplicant>();
}
