using DotNet.Domain.Core.Interfaces.Repositories;
using DotNet.Domain.Core.Interfaces.Services;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Pagination;
using DotNet.Domain.Service.Base;

namespace DotNet.Domain.Service
{
    public class AlunoService : ServiceBase, IAlunoService
    {
        private IAlunoRepository _alunoRepository { get; set; }

        public AlunoService(IAlunoRepository alunoRepository, INotifier notifier) : base(notifier)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<IEnumerable<Aluno>> GetAll()
        {
            return await _alunoRepository.GetAll();
        }

        public async Task<PagedList<Aluno>> GetAllPaginated(int page, int size)
        {
            return await _alunoRepository.GetAllPaginated(page, size);
        }

        public async Task<Aluno> GetById(int id)
        {
            return await _alunoRepository.GetById(id);
        }

        public async Task<Aluno> Post(Aluno aluno)
        {
            return await _alunoRepository.Post(aluno);
        }

        public async Task<Aluno> Put(Aluno aluno)
        {
            return await _alunoRepository.Put(aluno);
        }

        public async Task<bool> PutStatus(Aluno aluno)
        {
            aluno.ChangeStatus();
            return await _alunoRepository.PutStatus(aluno);
        }

        public async Task<bool> Delete(int id)
        {
            return await _alunoRepository.Delete(id);
        }

        public async Task<bool> InsertAlunoTurma(int alunoId, int turmaId)
        {
            return await _alunoRepository.InsertAlunoTurma(alunoId, turmaId);
        }

        public async Task<bool> DeleteAlunoTurma(int alunoId, int turmaId)
        {
            return await _alunoRepository.DeleteAlunoTurma(alunoId, turmaId);
        }

        public bool AlunoTurmaValidator(int alunoId, int turmaId)
        {
            return _alunoRepository.AlunoTurmaValidator(alunoId, turmaId);
        }
    }
}