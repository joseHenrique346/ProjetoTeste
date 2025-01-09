using Microsoft.EntityFrameworkCore.Storage;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction dbContextTransaction;
    private readonly AppDbContext _context = context;
    public void BeginTransaction()
    {
        dbContextTransaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.SaveChanges();
        dbContextTransaction.Commit();
        dbContextTransaction.Dispose();
    }
}