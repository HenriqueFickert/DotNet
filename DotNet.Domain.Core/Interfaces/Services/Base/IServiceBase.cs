using DotNet.Domain.Entities.Base;
using DotNet.Domain.Pagination;

namespace DotNet.Domain.Core.Interfaces.Services.Base
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<PagedList<TEntity>> GetAllPaginated(int page, int size);

        Task<TEntity> GetById(int id);

        Task<TEntity> Post(TEntity entity);

        Task<TEntity> Put(TEntity entity);

        Task<bool> PutStatus(TEntity entity);

        Task<bool> Delete(int id);
    }
}