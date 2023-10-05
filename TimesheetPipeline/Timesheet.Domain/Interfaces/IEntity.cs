namespace Timesheet.Domain.Interfaces
{
    /// <summary>
    /// Interface qui doit étendre toutes les Entity concrètes.
    /// </summary>
    /// <typeparam name="TKey">Type que doit implémenter l'Entity</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Id de l'entity.
        /// </summary>
        TKey Id { get; set; }
    }
}
