namespace prog_ing_azrv.Server.Models
{
    public class SkillCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Skill> Skills{ get; set; }
    }
}
