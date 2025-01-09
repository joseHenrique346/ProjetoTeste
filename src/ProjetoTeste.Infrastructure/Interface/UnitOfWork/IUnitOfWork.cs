namespace ProjetoTeste.Infrastructure.Interface.UnitOfWork;

public interface IUnitOfWork
{
    void BeginTransaction();
    void Commit();
}