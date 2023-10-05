namespace Timesheet.Domain.Interfaces
{
    /// <summary>
    /// Interface qui doit étendre les Repository pour toutes les méthodes liés à la lecture de données.
    /// </summary>
    /// <typeparam name="TEntity">Objet Entity à implémenter dans le Repository qui y sera utilisé.</typeparam>
    /// <typeparam name="TKey">Type de l'id de l'Entity à implémenter. Voir la définition de l'Entity en cas de doute.</typeparam>
    public interface IReaderRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Méthode à implémenter servant à récupérer tous les entity du repository.
        /// </summary>
        /// <returns>Un IEnumerable des entités implémentées.</returns>
        public IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Méthode à implémenter servant à récupérer une entity particulière du repository selon son id. 
        /// </summary>
        /// <param name="id">Identifiant de l'entity à récupérer</param>
        /// <returns>Un Entity.</returns>
        public TEntity GetById(TKey id);
    }
}