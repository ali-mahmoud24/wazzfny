using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<RequestApplicant> RequestApplicants { get; set; } = new List<RequestApplicant>();
}
