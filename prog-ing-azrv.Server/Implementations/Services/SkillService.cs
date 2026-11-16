using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using prog_ing_azrv.Server.DTOs.Skills;
using prog_ing_azrv.Server.Interfaces.Repos;
using prog_ing_azrv.Server.Interfaces.Services;
using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Implementations.Services
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork uow;
        private IMapper mapper;
        public SkillService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task DeleteSkill(string skillName)
        {
            Skill skill = await uow.Skills.GetSkillWithDetailsByName(skillName);
            await uow.Skills.DeleteSkill(skill);
        }

        public async Task<ICollection<SkillDTO>> GetAllSkills()
        {
            var skills = await uow.Skills.GetAllSkillsWithCategories();
            return mapper.Map<ICollection<SkillDTO>>(skills);
        }

        public async Task InsertSkill(SkillDTO skillDTO)
        {
            Skill skill = new Skill() { Name = skillDTO.Name, SkillCategoryID = 1}; //placeholder
            await uow.Skills.InsertSkillAsync(skill);
        }
    }
}
