namespace PointAppWithCleanArchitecture.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsyncWithString(string id);

        Task CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsyncWithString(string id);
        Task UpdateAsync(Guid id);
        Task UpdateAsyncWithString(string id);

    }
}