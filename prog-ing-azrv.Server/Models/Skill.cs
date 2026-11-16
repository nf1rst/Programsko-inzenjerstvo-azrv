using prog_ing_azrv.Server.Models.M2N;

namespace prog_ing_azrv.Server.Models
{
    public class Skill
    {
        public int Id{ get; set; }
        public string Name { get; set; } = String.Empty;
        public int? SkillCategoryID{ get; set; }
        public SkillCategory? SkillCategory{ get; set; }
        public ICollection<UserSkill> UserSkills{ get; set; }=new List<UserSkill>();
    }
}
