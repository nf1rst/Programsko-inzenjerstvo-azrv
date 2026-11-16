using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Interfaces.Repos;

namespace prog_ing_azrv.Server.Implementations.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AzrvContext context;
        public IUserRepo Users { get; }
        public UnitOfWork(
            AzrvContext context,
            IUserRepo users
            )
        {
            this.context = context;
            this.Users = users;
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
