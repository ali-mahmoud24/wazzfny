namespace ApplicantAPI.Dtos.ComputerSkill;

public class GetComputerSkillDto
{
    public int ComputerSkillId { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
