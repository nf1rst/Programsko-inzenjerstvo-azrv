using prog_ing_azrv.Server.DTOs.User;

namespace prog_ing_azrv.Server.Interfaces.Services
{
    public interface IUserSErvice
    {
        Task<UserDTO> GetUserByUsername(string username);
        Task DeleteUser(string username);
        Task InserUser(string username, string password, string bio);
    }
}
