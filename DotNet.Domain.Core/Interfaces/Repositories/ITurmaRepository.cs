using DotNet.Domain.Core.Interfaces.Repositories.Base;
using DotNet.Domain.Entities;
using DotNet.Domain.Pagination;

namespace DotNet.Domain.Core.Interfaces.Repositories
{
    public interface ITurmaRepository : IRepositoryBase<Turma>
    {
        Task<EntityPaged<Turma>> GetDetails(int id, int page, int pageSize);

        bool TurmaNameValidator(string name, int id);
    }
}