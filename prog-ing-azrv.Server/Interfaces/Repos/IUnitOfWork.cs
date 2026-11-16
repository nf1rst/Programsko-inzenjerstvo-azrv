namespace prog_ing_azrv.Server.Interfaces.Repos
{
    public interface IUnitOfWork:IDisposable
    {
        public IUserRepo Users{ get; }

        Task<int> SaveChangesAsync();
    }
}
