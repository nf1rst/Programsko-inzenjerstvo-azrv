using prog_ing_azrv.Server.DTOs;
using prog_ing_azrv.Server.Interfaces.Repos;
using prog_ing_azrv.Server.Interfaces.Services;
using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Implementations.Services
{
    public class UserService : IUserSErvice
    {
        private readonly IUnitOfWork uow;
        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task DeleteUser(string username)
        {
            var user = await uow.Users.GetUserByUsername(username);
            await uow.Users.DeleteUser(user);
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var user = await uow.Users.GetUserByUsername(username);
            UserDTO dto = new UserDTO() { Username = user.UserName, Bio = user.Bio };
            return dto;
        }

        public async Task InserUser(string username, string password, string bio)
        {
            var user  = new User() {  UserName = username, Bio = bio };
            await uow.Users.InsertUser(user);
        }
    }
}
