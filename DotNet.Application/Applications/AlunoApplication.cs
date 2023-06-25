using AutoMapper;
using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Dtos.AlunoTurma;
using DotNet.Application.Dtos.Pagination;
using DotNet.Application.Interfaces;
using DotNet.Application.Utilities.Hash;
using DotNet.Domain.Core.Interfaces.Services;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Enums;
using DotNet.Domain.Pagination;

namespace DotNet.Application.Applications
{
    public class AlunoApplication : IAlunoApplication
    {
        private readonly IAlunoService _alunoService;
        protected readonly IMapper _mapper;
        protected readonly INotifier _notifier;

        public AlunoApplication(IAlunoService alunoService,
                                INotifier notifier,
                                IMapper mapper)
        {
            _alunoService = alunoService;
            _mapper = mapper;
            _notifier = notifier;
        }

        public async Task<IEnumerable<ViewAlunoDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ViewAlunoDto>>(await _alunoService.GetAll());
        }

        public async Task<ViewPaginationDto<Aluno, ViewAlunoDto>> GetAllPaginated(int page, int size)
        {
            PagedList<Aluno> pagedList = await _alunoService.GetAllPaginated(page, size);

            if (pagedList is null)
                return null;

            return new ViewPaginationDto<Aluno, ViewAlunoDto>(pagedList, _mapper.Map<List<ViewAlunoDto>>(pagedList));
        }

        public async Task<ViewAlunoDto> GetById(int id)
        {
            return _mapper.Map<ViewAlunoDto>(await _alunoService.GetById(id));
        }

        public async Task<ViewAlunoDto> Post(PostAlunoDto postAlunoDto)
        {
            postAlunoDto.Senha = HashManager.ConvertToHash(postAlunoDto.Senha);
            Aluno alunoResult = await _alunoService.Post(_mapper.Map<Aluno>(postAlunoDto));
            return _mapper.Map<ViewAlunoDto>(alunoResult);
        }

        public async Task<ViewAlunoDto> Put(PutAlunoDto putAlunoDto)
        {
            putAlunoDto.Senha = HashManager.ConvertToHash(putAlunoDto.Senha);
            Aluno alunoResult = await _alunoService.Put(_mapper.Map<Aluno>(putAlunoDto));
            return _mapper.Map<ViewAlunoDto>(alunoResult);
        }

        public async Task<bool> PutStatus(int id, EStatus status)
        {
            return await _alunoService.PutStatus(_mapper.Map<Aluno>(new ViewAlunoDto(id, status)));
        }

        public async Task<bool> Delete(int id)
        {
            return await _alunoService.Delete(id);
        }

        public async Task<PutAlunoDto> GetPutData(int id)
        {
            return _mapper.Map<PutAlunoDto>(await _alunoService.GetById(id));
        }

        public async Task<bool> InsertAlunoTurma(PostAlunoTurmaDto postAlunoTurmaDto)
        {
            return await _alunoService.InsertAlunoTurma(postAlunoTurmaDto.AlunoSelecionadoId, postAlunoTurmaDto.TurmaSelecionadaId);
        }

        public async Task<bool> DeleteAlunoTurma(int alunoId, int turmaId)
        {
            return await _alunoService.DeleteAlunoTurma(alunoId, turmaId);
        }

        public bool AlunoTurmaValidator(int alunoId, int turmaId)
        {
            return _alunoService.AlunoTurmaValidator(alunoId, turmaId);
        }
    }
}