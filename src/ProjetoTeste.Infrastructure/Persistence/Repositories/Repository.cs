using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        //protected readonly IProductRepository _productRepository;
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context/*,IProductRepository productRepository*/)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            //_productRepository = productRepository;
        }

        public TEntity? Get(long id)
        {
            return _dbSet.Find(id);
        }

        public List<TEntity?> GetAll()
        {
            return _dbSet.ToList();
        }

        //public async Task<ProductRepository> GetWithIncludesAsync()
        //{
        //    await _productRepository.Get
        //}

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public bool Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return true;
        }
    }
}
