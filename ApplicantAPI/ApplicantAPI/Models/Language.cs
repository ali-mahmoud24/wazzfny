using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class Language
{
    public byte LanguageId { get; set; }

    public string LanguageName { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<RequestApplicantLanguage> RequestApplicantLanguages { get; set; } = new List<RequestApplicantLanguage>();
}
