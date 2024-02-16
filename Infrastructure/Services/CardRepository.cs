using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Utils;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly CardDbContext _dbContext;
        private readonly IMapper _mapper;

        public CardRepository(CardDbContext dbContext, IMapper mapper, IDateTimeFactory dateTimeFactory) : base(dbContext, mapper, dateTimeFactory)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<TTarget?> GetSingleCardById<TTarget>(Guid cardId, bool isAdmin, string userId) where TTarget : class
        {
            IQueryable<Card> queryable = _dbContext.Cards.OrderBy(c => c.CreatedDate);

            queryable = QueryBuilder(queryable, cardId, isAdmin, userId);

            var result = queryable.Select(c=> _mapper.Map<TTarget>(c));

           return await result.FirstOrDefaultAsync();

        }

        public async Task<Pagination<TTarget>> GetCards<TTarget>(QueryParams queryParams, CardFilters filters, bool isAdmin, string userId) where TTarget : class
        {
            IQueryable<Card> queryable = _dbContext.Cards.OrderBy(c => c.CreatedDate);

            queryable = QueryBuilder(queryable, queryParams, filters, isAdmin, userId);

            var count = queryable.Count();

            var results = (await queryable
                .Select(c => _mapper.Map<TTarget>(c))
                .Skip(queryParams.PageSize * (queryParams.PageIndex - 1))
                .Take(queryParams.PageSize)
                .ToListAsync());

            return new Pagination<TTarget>
            {
                Count = count,
                Data = results,
                PageIndex = queryParams.PageIndex,
                PageSize = queryParams.PageSize
            };
        }

        private static IQueryable<Card> QueryBuilder(IQueryable<Card> queryable, Guid cardId, bool isAdmin, string userId)
        {
            queryable = queryable.Where(c => c.Id.Equals(cardId));

            if (!isAdmin)
            {
                queryable = queryable.Where(c => c.OwnerId.Equals(userId));
            }

            return queryable;
        }
        private static IQueryable<Card> QueryBuilder(IQueryable<Card> queryable, QueryParams queryParams, CardFilters filters, bool isAdmin, string userId)
        {
            if (!isAdmin)
            {
                queryable = queryable.Where(c => c.OwnerId.Equals(userId));
            }

            if (filters.Name is not null)
            {
                queryable = queryable.Where(c => c.Name.ToUpper().Equals(filters.Name.ToUpper()));
            }

            if (filters.Color is not null)
            {
                queryable = queryable.Where(c => c.Color.ToUpper().Equals(filters.Color.ToUpper()));
            }

            if (filters.Status is not null)
            {
                queryable = queryable.Where(c => c.Status.Equals(filters.Status));
            }

            if (filters.CreatedDate is not null)
            {
                queryable = queryable.Where(c => c.CreatedDate.Equals(filters.CreatedDate));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Search))
            {
                queryable = queryable.Where(c => c.Name.Contains(queryParams.Search)
                                                 || c.Color.Contains(queryParams.Search));
            }

            return queryable;
        }

    }
}
