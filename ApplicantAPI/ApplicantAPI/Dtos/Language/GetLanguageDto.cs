namespace ApplicantAPI.Dtos.Language;

public class GetLanguageDto
{
    public int LanguageId { get; set; }
    public string LanguageName { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
