using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Interfaces.Repos
{
    public interface ISkillRepo
    {
        Task InsertSkillAsync(Skill skill);
        Task DeleteSkill(Skill skill);
        Task<ICollection<Skill>> GetAllSkillsWithCategories();
        Task<Skill> GetSkillWithDetailsByName(string name);

    }
}
