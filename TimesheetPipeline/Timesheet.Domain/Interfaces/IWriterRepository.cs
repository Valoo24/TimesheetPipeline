namespace Timesheet.Domain.Interfaces
{
    public interface IWriterRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public TKey Add(TEntity entity);
        public TKey Update(TEntity entity);
        public TKey Delete(TKey id);
    }
}