using Microsoft.EntityFrameworkCore;
using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Interfaces.Repos;
using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Implementations.Repos
{
    public class SkillRepo : ISkillRepo
    {
        private readonly AzrvContext context;
        public SkillRepo(AzrvContext context)
        {
            this.context = context;
        }
        public Task DeleteSkill(Skill skill)
        {
            context.Remove(skill);
            return Task.CompletedTask;
        }

        public async Task<ICollection<Skill>> GetAllSkillsWithCategories()
        {
            return await context.Skills
                .Include(s => s.SkillCategory)
                .ToListAsync();
        }

        public async Task<Skill> GetSkillWithDetailsByName(string name)
        {
            return await context.Skills.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task InsertSkillAsync(Skill skill)
        {
            await context.Skills.AddAsync(skill);
        }
    }
}
