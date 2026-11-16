using Microsoft.EntityFrameworkCore;
using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Interfaces.Repos;
using prog_ing_azrv.Server.Models;

namespace prog_ing_azrv.Server.Implementations.Repos
{
    public class UserRepo:IUserRepo
    {
        private readonly AzrvContext context;
        public UserRepo(AzrvContext context)
        {
            this.context = context;
        }

        public Task DeleteUser(User user)
        {
            context.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await context.Users.FirstOrDefaultAsync(u=>u.UserName==username);
        }

        public async Task InsertUser(User user)
        {
            await context.Users.AddAsync(user);
        }
    }
}
