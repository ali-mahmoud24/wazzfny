using System;
using System.Collections.Generic;

namespace ApplicantAPI.Models;

public partial class RequestApplicantLanguage
{
    public int ReqLangId { get; set; }

    public int RequestId { get; set; }

    public byte LanguageId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual RequestApplicant Request { get; set; } = null!;
}
