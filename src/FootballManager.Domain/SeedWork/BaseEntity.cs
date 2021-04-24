namespace FootballManager.Domain.SeedWork
{

    /// <summary>
    /// A base object for entities. For all entities will have an integer Id
    /// </summary>
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Tells if the item is already persisted or if it is a memory-only item
        /// </summary>
        /// <returns>True if the object was not persisted</returns>
        public bool IsTransient()
        {
            return this.Id == default;
        }
    }
}
