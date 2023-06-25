using DotNet.Domain.Core.Interfaces.Services.Base;
using DotNet.Domain.Entities;
using DotNet.Domain.Pagination;

namespace DotNet.Domain.Core.Interfaces.Services
{
    public interface ITurmaService : IServiceBase<Turma>
    {
        Task<EntityPaged<Turma>> GetDetails(int id, int page, int pageSize);

        bool TurmaNameValidator(string name, int id);
    }
}