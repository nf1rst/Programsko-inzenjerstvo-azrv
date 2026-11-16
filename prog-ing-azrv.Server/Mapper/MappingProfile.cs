using AutoMapper;
using prog_ing_azrv.Server.DTOs.Skills;
using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Mapper
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Skill, SkillDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SkillCategory.Name));
        }
    }
}
