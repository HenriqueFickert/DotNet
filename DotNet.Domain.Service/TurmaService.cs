using DotNet.Domain.Core.Interfaces.Repositories;
using DotNet.Domain.Core.Interfaces.Services;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Pagination;
using DotNet.Domain.Service.Base;

namespace DotNet.Domain.Service
{
    public class TurmaService : ServiceBase, ITurmaService
    {
        private ITurmaRepository _turmaRepository { get; set; }

        public TurmaService(ITurmaRepository turmaRepository, INotifier notifier) : base(notifier)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<IEnumerable<Turma>> GetAll()
        {
            return await _turmaRepository.GetAll();
        }

        public async Task<PagedList<Turma>> GetAllPaginated(int page, int size)
        {
            return await _turmaRepository.GetAllPaginated(page, size);
        }

        public async Task<Turma> GetById(int id)
        {
            return await _turmaRepository.GetById(id);
        }

        public async Task<Turma> Post(Turma turma)
        {
            return await _turmaRepository.Post(turma);
        }

        public async Task<Turma> Put(Turma turma)
        {
            return await _turmaRepository.Put(turma);
        }

        public async Task<bool> PutStatus(Turma turma)
        {
            turma.ChangeStatus();
            return await _turmaRepository.PutStatus(turma);
        }

        public async Task<bool> Delete(int id)
        {
            return await _turmaRepository.Delete(id);
        }

        public async Task<EntityPaged<Turma>> GetDetails(int id, int page, int pageSize)
        {
            return await _turmaRepository.GetDetails(id, page, pageSize);
        }

        public bool TurmaNameValidator(string name, int id)
        {
            return _turmaRepository.TurmaNameValidator(name, id);
        }
    }
}