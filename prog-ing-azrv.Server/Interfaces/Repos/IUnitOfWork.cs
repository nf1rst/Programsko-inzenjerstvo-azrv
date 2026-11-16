namespace prog_ing_azrv.Server.Interfaces.Repos
{
    public interface IUnitOfWork:IDisposable
    {
        public IUserRepo Users{ get; }
        public ISkillRepo Skills { get; }

        Task<int> SaveChangesAsync();
    }
}
