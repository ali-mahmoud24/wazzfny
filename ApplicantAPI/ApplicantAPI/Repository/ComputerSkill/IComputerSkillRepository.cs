using ApplicantAPI.Dtos.ComputerSkill;
using ApplicantAPI.Models;

namespace ApplicantAPI.Repository;

public interface IComputerSkillRepository
{
    Task<Response<IEnumerable<GetComputerSkillDto>>> GetAllComputerSkills();
    Task<Response<GetComputerSkillDto>> AddComputerSkill(ComputerSkill newComputerSkill);

    Task<Response<int>> GetComputerSkillsCount();
}