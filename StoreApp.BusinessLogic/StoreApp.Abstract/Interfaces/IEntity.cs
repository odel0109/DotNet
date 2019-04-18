namespace StoreApp.Abstract.Interfaces
{
    /// <summary>
    /// Objects stored at reository must implement IEntity interface
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Returns primary key of object
        /// </summary>
        object[] PrimaryKey { get; }
    }
}
