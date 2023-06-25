using DotNet.Application.Dtos.Pagination;
using DotNet.Application.Dtos.Turma;
using DotNet.Domain.Entities;
using DotNet.Domain.Enums;

namespace DotNet.Application.Interfaces
{
    public interface ITurmaApplication
    {
        Task<IEnumerable<ViewTurmaDto>> GetAll();

        Task<ViewPaginationDto<Turma, ViewTurmaDto>> GetAllPaginated(int page, int size);

        Task<ViewTurmaDto> GetById(int id);

        Task<ViewTurmaDto> Post(PostTurmaDto postTurmaDto);

        Task<ViewTurmaDto> Put(PutTurmaDto putTurmaDto);

        Task<bool> PutStatus(int id, EStatus status);

        Task<bool> Delete(int id);

        Task<PutTurmaDto> GetPutData(int id);

        Task<ViewEntityPaginationDto<Turma, ViewTurmaDetailsDto>> GetDetails(int id, int page, int pageSize);

        bool TurmaNameValidator(string name, int id);
    }
}