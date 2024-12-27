namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(long id);
        Task<T> CreateAsync(T entity);
        Task<bool> Update(T entity);
        Task<bool> DeleteAsync(long id);
    }
}