namespace prog_ing_azrv.Server.Models.M2N
{
    public class UserUser //friendship
    {
        public int Id{ get; set; }
        public int FirstUserID{ get; set; }
        public int SecondUserID{ get; set; }
        public User SecondUser{ get; set; }
        public User FirstUser{ get; set; }
    }
}
