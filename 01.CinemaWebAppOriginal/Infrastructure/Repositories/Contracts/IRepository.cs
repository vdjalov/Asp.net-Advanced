namespace CinemaWebAppOriginal.Infrastructure.Repositories.Contracts
{
    public interface IRepository<TType, TId> where TType : class 
    {


        // GET an entity of type TType by its ID not asynchronous
        TType GetById(TId id);

        // GET an entity of type TType by its ID
        Task<TType> GetByIdAsync(TId id);


        // GET all entities of tyope TType not asynchronous
        IEnumerable<TType> GetAll();

        // GET all entities of tyope TType 
        Task<ICollection<TType>> GetAllAsync();

        // GET all entities of tyope TType 
        IQueryable<TType> GetAllAttached();

        // Add a new Entity of type TType not asynchronous
        void Add(TType entity);

        // Add a new Entity of type TType
        Task AddAndSaveAsync(TType entity);

        // Delete an Entity of type TType by its ID
        bool Delete(TId id);

        // Delete an Entity of type TType by its ID
        Task DeleteAsync(TId id);

        void DeleteRange(List<TType> entities);

        // Delete by bool value actualy not deleted - not asynchronous
        //bool SoftDelete(TId id);

        // Delete by bool value actualy not deleted
        //Task SoftDeleteAsync(TId id);

        // Update an existing entity of type TType not asynchronous
        void Update(TType entity);

        // Update an existing entity of type TType
        Task UpdateAsync(TType entity);
    }
}
