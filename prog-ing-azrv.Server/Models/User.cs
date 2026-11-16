using Microsoft.AspNetCore.Identity;
using prog_ing_azrv.Server.Models.M2N;

namespace prog_ing_azrv.Server.Models
{
    public class User:IdentityUser
    {
        public string? Bio{ get; set; }
        public ICollection<UserUser> FriendshipsFirst{ get; set; }= new List<UserUser>();
        public ICollection<UserUser> FriendshipsSecond{ get; set; }= new List<UserUser>();
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
