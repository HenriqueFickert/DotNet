using DotNet.Domain.Core.Interfaces.Repositories.Base;
using DotNet.Domain.Entities;

namespace DotNet.Domain.Core.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryBase<Aluno>
    {
        Task<bool> InsertAlunoTurma(int alunoId, int turmaId);

        Task<bool> DeleteAlunoTurma(int alunoId, int turmaId);

        bool AlunoTurmaValidator(int alunoId, int turmaId);
    }
}