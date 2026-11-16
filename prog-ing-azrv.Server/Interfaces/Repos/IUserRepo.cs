using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Interfaces.Repos
{
    public interface IUserRepo
    {
        Task<User> GetUserByUsername(string username);
        Task InsertUser(User user);
        Task DeleteUser(User user);
    }
}
