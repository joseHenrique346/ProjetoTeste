using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    private readonly IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
    private readonly AppDbContext _context;
    public async Task CommitAsync()
    {
        await context.Database.CommitTransactionAsync();
    }
}
