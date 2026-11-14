using Microsoft.AspNetCore.Identity;

namespace prog_ing_azrv.Server.Models
{
    public class User:IdentityUser
    {
        public string? Username { get; set; } = String.Empty;
        public string? Bio{ get; set; }
    }
}
