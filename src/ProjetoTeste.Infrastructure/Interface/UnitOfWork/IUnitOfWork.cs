namespace ProjetoTeste.Infrastructure.Interface.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}