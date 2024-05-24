using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class RequestApplicantSkill
{
    public int ReqSkillComId { get; set; }

    public byte ComputerSkillId { get; set; }

    public int RequestId { get; set; }

    public virtual ComputerSkill ComputerSkill { get; set; } = null!;

    public virtual RequestApplicant Request { get; set; } = null!;
}
