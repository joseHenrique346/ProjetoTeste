using Microsoft.EntityFrameworkCore.Storage;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext context): IUnitOfWork
    {
        private readonly IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
        private readonly AppDbContext _context;
        public async Task CommitAsync()
        {
            await context.Database.CommitTransactionAsync();
        }
        public void Commit()
        {
            context.Database.CommitTransaction();
        }
    }
}
