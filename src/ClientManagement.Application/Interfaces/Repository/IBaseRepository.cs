namespace ClientManagement.Application.Interfaces.Repository
{
    public interface IBaseRepository<T, TId> where T : class
    {
        Task<T> GetByIdAsync(TId id);
        Task<IList<T>> GetAllAsync();
        Task<T> SaveAsync(T t);
        Task<T> UpdateAsync(T t);
        Task<T> DeleteAsync(TId id);
    }
}
