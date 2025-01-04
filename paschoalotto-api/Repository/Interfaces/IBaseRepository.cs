namespace paschoalotto_api.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T?> GetByIdAsync(int id);
        
        Task<T> InsertAsync(T entity);
        
        Task<T> UpdateAsync(T entity);
        
        Task DeleteAsync(int id); }
}
