using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<TEntity?>> GetListByListIdFind(List<long> id)
        {
            var findByListId = (from i in id
                                select _dbSet.Find(i)).ToList();
            return findByListId;
        }

        public async Task<List<TEntity?>> GetListByListIdWhere(List<long> listId)
        {
            var findByListId = await _dbSet.Where(i => listId.Contains(i.Id)).ToListAsync();
            return findByListId;
        }

        public async Task<List<TEntity>> CreateAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<List<TEntity>> Update(List<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(List<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}