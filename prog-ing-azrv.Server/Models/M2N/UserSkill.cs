using prog_ing_azrv.Server.Models.Enums;

namespace prog_ing_azrv.Server.Models.M2N
{
    public class UserSkill
    {
        public int Id { get; set; }
        public int SkillID{ get; set; }
        public Skill Skill{ get; set; }
        public string UserID{ get; set; }
        public User User{ get; set; }
        public SkillLevel SkillLevel{ get; set; }
        public int SkillLevelID{ get; set; }
        public WantedStatus WantedStatus{ get; set; }
        public int WantedStatusId{ get; set; }
    }
}
