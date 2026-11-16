using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Interfaces.Repos;
using prog_ing_azrv.Server.Interfaces.Services;

namespace prog_ing_azrv.Server.Implementations.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AzrvContext context;
        public IUserRepo Users { get; }
        public ISkillRepo Skills { get; }
        public UnitOfWork(
            AzrvContext context,
            IUserRepo users,
            ISkillRepo skills
            )
        {
            this.context = context;
            this.Users = users;
            this.Skills = skills;
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
