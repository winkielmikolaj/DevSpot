namespace DevSpot.Repositories
{
    public interface IRepository<T> where T : class //where T : class to jest constraint
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        //T entity = job postings
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
