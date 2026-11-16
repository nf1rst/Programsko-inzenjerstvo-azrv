using prog_ing_azrv.Server.Models.M2N;

namespace prog_ing_azrv.Server.Models.Enums
{
    public class SkillLevel
    {
        //beginner, intermediate, pro
        public int Id{ get; set; }
        public string? Name{ get; set; }
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
