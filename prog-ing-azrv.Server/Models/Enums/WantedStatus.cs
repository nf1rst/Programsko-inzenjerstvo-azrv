using prog_ing_azrv.Server.Models.M2N;

namespace prog_ing_azrv.Server.Models.Enums
{
    public class WantedStatus
    {
        //want, offer
        public int Id{ get; set; }
        public string? Name{ get; set; }
        public ICollection<UserSkill> UserSkill{ get; set; }=new List<UserSkill>();
    }
}
