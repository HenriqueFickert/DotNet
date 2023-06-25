using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Dtos.AlunoTurma;
using DotNet.Application.Dtos.Pagination;
using DotNet.Domain.Entities;
using DotNet.Domain.Enums;

namespace DotNet.Application.Interfaces
{
    public interface IAlunoApplication
    {
        Task<IEnumerable<ViewAlunoDto>> GetAll();

        Task<ViewPaginationDto<Aluno, ViewAlunoDto>> GetAllPaginated(int page, int size);

        Task<ViewAlunoDto> GetById(int id);

        Task<ViewAlunoDto> Post(PostAlunoDto postAlunoDto);

        Task<ViewAlunoDto> Put(PutAlunoDto putAlunoDto);

        Task<bool> PutStatus(int id, EStatus status);

        Task<bool> Delete(int id);

        Task<PutAlunoDto> GetPutData(int id);

        Task<bool> InsertAlunoTurma(PostAlunoTurmaDto postAlunoTurmaDto);

        Task<bool> DeleteAlunoTurma(int alunoId, int turmaId);

        bool AlunoTurmaValidator(int alunoId, int turmaId);
    }
}