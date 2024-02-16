using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly CardDbContext _dbContext;
        private readonly IDateTimeFactory _dateTimeFactory;

        public BaseRepository(CardDbContext dbContext, IMapper mapper, IDateTimeFactory dateTimeFactory)
        {
            _dateTimeFactory = dateTimeFactory;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(TEntity entity)
        {
            entity.CreatedDate = _dateTimeFactory.Now();

            _dbContext.Set<TEntity>().Add(entity);
        }

        public TEntity? Get(Guid entityId)
        {
            return _dbContext.Set<TEntity>().Find(entityId);
        }

        public TTarget? Get<TTarget>(Guid entityId) where TTarget : class
        {
            return _dbContext.Set<TEntity>().Where(t => t.Id.Equals(entityId))
                .ProjectTo<TTarget>(_mapper.ConfigurationProvider).FirstOrDefault();
        }

        public List<TTarget> GetAll<TTarget>() where TTarget : class
        {
            return _dbContext.Set<TEntity>().ProjectTo<TTarget>(_mapper.ConfigurationProvider).ToList();
        }

        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TTarget>> GetAllAsync<TTarget>() where TTarget : class
        {
            return await _dbContext.Set<TEntity>().ProjectTo<TTarget>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TTarget?> GetAsync<TTarget>(Guid entityId) where TTarget : class
        {
            return await _dbContext.Set<TEntity>().Where(t => t.Id.Equals(entityId))
                .ProjectTo<TTarget>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid entityId)
        {
            return await _dbContext.Set<TEntity>().Where(t => t.Id.Equals(entityId)).FirstOrDefaultAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}