using DotNet.Domain.Core.Interfaces.Services.Base;
using DotNet.Domain.Entities;

namespace DotNet.Domain.Core.Interfaces.Services
{
    public interface IAlunoService : IServiceBase<Aluno>
    {
        Task<bool> InsertAlunoTurma(int alunoId, int turmaId);

        Task<bool> DeleteAlunoTurma(int alunoId, int turmaId);

        bool AlunoTurmaValidator(int alunoId, int turmaId);
    }
}