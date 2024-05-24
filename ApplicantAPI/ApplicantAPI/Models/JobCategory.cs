using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class JobCategory
{
    public int JobCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
