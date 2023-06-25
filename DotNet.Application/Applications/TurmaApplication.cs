using AutoMapper;
using DotNet.Application.Dtos.Pagination;
using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Interfaces.Services;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Enums;
using DotNet.Domain.Pagination;

namespace DotNet.Application.Applications
{
    public class TurmaApplication : ITurmaApplication
    {
        private readonly ITurmaService _turmaService;
        protected readonly IMapper _mapper;
        protected readonly INotifier _notifier;

        public TurmaApplication(ITurmaService turmaService,
                               INotifier notifier,
                               IMapper mapper)
        {
            _turmaService = turmaService;
            _mapper = mapper;
            _notifier = notifier;
        }

        public async Task<IEnumerable<ViewTurmaDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ViewTurmaDto>>(await _turmaService.GetAll());
        }

        public async Task<ViewPaginationDto<Turma, ViewTurmaDto>> GetAllPaginated(int page, int size)
        {
            PagedList<Turma> pagedList = await _turmaService.GetAllPaginated(page, size);

            if (pagedList is null)
                return null;

            return new ViewPaginationDto<Turma, ViewTurmaDto>(pagedList, _mapper.Map<List<ViewTurmaDto>>(pagedList));
        }

        public async Task<ViewTurmaDto> GetById(int id)
        {
            return _mapper.Map<ViewTurmaDto>(await _turmaService.GetById(id));
        }

        public async Task<ViewTurmaDto> Post(PostTurmaDto postTurmaDto)
        {
            return _mapper.Map<ViewTurmaDto>(await _turmaService.Post(_mapper.Map<Turma>(postTurmaDto)));
        }

        public async Task<ViewTurmaDto> Put(PutTurmaDto PutTurmaDto)
        {
            return _mapper.Map<ViewTurmaDto>(await _turmaService.Put(_mapper.Map<Turma>(PutTurmaDto)));
        }

        public async Task<bool> PutStatus(int id, EStatus status)
        {
            return await _turmaService.PutStatus(_mapper.Map<Turma>(new ViewTurmaDto(id, status)));
        }

        public async Task<bool> Delete(int id)
        {
            return await _turmaService.Delete(id);
        }

        public async Task<PutTurmaDto> GetPutData(int id)
        {
            return _mapper.Map<PutTurmaDto>(await _turmaService.GetById(id));
        }

        public async Task<ViewEntityPaginationDto<Turma, ViewTurmaDetailsDto>> GetDetails(int id, int page, int pageSize)
        {
            EntityPaged<Turma> pagedEntity = await _turmaService.GetDetails(id, page, pageSize);

            if (pagedEntity is null)
                return null;

            return new ViewEntityPaginationDto<Turma, ViewTurmaDetailsDto>(pagedEntity, _mapper.Map<ViewTurmaDetailsDto>(pagedEntity.Entity));
        }

        public bool TurmaNameValidator(string name, int id)
        {
            return _turmaService.TurmaNameValidator(name, id);
        }
    }
}