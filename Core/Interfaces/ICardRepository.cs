using Core.Entities;
using Core.Models;
using Core.Utils;

namespace Core.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<TTarget?> GetSingleCardById<TTarget>(Guid cardId, bool isAdmin, string userId) where TTarget : class;
        Task<Pagination<TTarget>> GetCards<TTarget>(QueryParams queryParams, CardFilters filters, bool isAdmin, string userId) where TTarget : class;
    }
}
