using Core.Entities;

namespace Core.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity? Get(Guid entityId);

        TTarget? Get<TTarget>(Guid entityId) where TTarget : class;

        Task<TTarget?> GetAsync<TTarget>(Guid entityId) where TTarget : class;

        List<TEntity> GetAll();

        List<TTarget> GetAll<TTarget>() where TTarget : class;

        Task<List<TEntity>> GetAllAsync();

        Task<List<TTarget>> GetAllAsync<TTarget>() where TTarget : class;

        Task<TEntity?> GetByIdAsync(Guid entityId);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        Task<int> CountAllAsync();
        
        int Complete();
        
        Task<int> CompleteAsync();
    }
}
