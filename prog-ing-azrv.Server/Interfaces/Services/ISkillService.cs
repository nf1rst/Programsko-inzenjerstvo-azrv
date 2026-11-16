using prog_ing_azrv.Server.DTOs.Skills;
using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Interfaces.Services
{
    public interface ISkillService
    {
        Task<ICollection<SkillDTO>> GetAllSkills();
        Task InsertSkill(SkillDTO skillDTO);
        Task DeleteSkill(string skillName);
    }
}
