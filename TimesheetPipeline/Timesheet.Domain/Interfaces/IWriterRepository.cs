namespace Timesheet.Domain.Interfaces
{
    public interface IWriterRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TKey> AddAsync(TEntity entity);
        Task<TKey> UpdateAsync(TEntity entity);
        Task<TKey> DeleteAsync(TKey id);
    }
}