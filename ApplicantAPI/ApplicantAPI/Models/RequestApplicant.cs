using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class RequestApplicant
{
    public int RequestId { get; set; }

    public DateTime PostDate { get; set; }

    public DateTime StartPublish { get; set; }

    public DateTime EndPublish { get; set; }

    public int JobId { get; set; }

    public byte ExperienceId { get; set; }

    public byte? AgeFrom { get; set; }

    public byte? AgeTo { get; set; }

    public byte? Gender { get; set; }

    public double? SalaryFrom { get; set; }

    public double? SalaryTo { get; set; }

    public bool? IsNegotiate { get; set; }

    public bool? WorkType { get; set; }

    public byte? WorkHour { get; set; }

    public string? Details { get; set; }

    public bool IsEnd { get; set; }

    public int UserId { get; set; }

    public virtual Experience Experience { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;

    public virtual ICollection<RequestApplicantLanguage> RequestApplicantLanguages { get; set; } = new List<RequestApplicantLanguage>();

    public virtual ICollection<RequestApplicantSkill> RequestApplicantSkills { get; set; } = new List<RequestApplicantSkill>();

    public virtual User User { get; set; } = null!;
}
