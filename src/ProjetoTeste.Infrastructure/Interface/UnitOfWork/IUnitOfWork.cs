namespace ProjetoTeste.Infrastructure.Interface.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}