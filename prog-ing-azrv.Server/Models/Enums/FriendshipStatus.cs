using prog_ing_azrv.Server.Models.M2N;

namespace prog_ing_azrv.Server.Models.Enums
{
    public class FriendshipStatus 
    {
        //pending, accepted, blocked
        public int Id{ get; set; }
        public string? Name{ get; set; }
        public ICollection<UserUser> UserUser { get; set; } = new List<UserUser>();
    }
}
