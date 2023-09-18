using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Interfaces
{
    /// <summary>
    /// Interface à implémenter dans les services concrets pour implémenter toutes les méthodes de lecture d'un service.
    /// </summary>
    /// <typeparam name="TEntity">Entity à implémenter dans le service.</typeparam>
    /// <typeparam name="Tkey">Id de l'Entity implémenter. En cas de doute, voir la définition de l'Entity à implémenter.</typeparam>
    public interface IReaderService<TEntity, Tkey> where TEntity : IEntity<Tkey>
    {
        /// <summary>
        /// Renvoie un IEnumerable d'Entity implémenté dans le service.
        /// </summary>
        /// <returns>Un IEnumerable d'Entity implémenté dans le service.</returns>
        public IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Renvoie un Entity selon son Id.
        /// </summary>
        /// <param name="id">Id de l'Entity à récupérer.</param>
        /// <returns>Un Entity du type implémenté dans le service.</returns>
        public TEntity GetById (Tkey id);
    }
}