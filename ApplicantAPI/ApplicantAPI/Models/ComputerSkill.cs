using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class ComputerSkill
{
    public byte ComputerSkillId { get; set; }

    public string SkillName { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<RequestApplicantSkill> RequestApplicantSkills { get; set; } = new List<RequestApplicantSkill>();
}
