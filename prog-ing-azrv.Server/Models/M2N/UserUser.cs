using prog_ing_azrv.Server.Models.Enums;

namespace prog_ing_azrv.Server.Models.M2N
{
    public class UserUser //friendship
    {
        public int Id{ get; set; }
        public string FirstUserID{ get; set; }
        public string SecondUserID{ get; set; }
        public User SecondUser{ get; set; }
        public User FirstUser{ get; set; }
        public int FriendhipStatusID{ get; set; }
        public FriendshipStatus FriendshipStatus{ get; set; }
    }
}
