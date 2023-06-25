using AutoMapper;
using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Dtos.Turma;
using DotNet.Domain.Entities;

namespace DotNet.Application.Mappings
{
    public class TurmaMappingProfile : Profile
    {
        public TurmaMappingProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<Turma, ViewTurmaDto>().ReverseMap();
            CreateMap<Turma, PostTurmaDto>().ReverseMap();
            CreateMap<Turma, PutTurmaDto>().ReverseMap();
            CreateMap<Turma, ViewTurmaDetailsDto>().ReverseMap();

            CreateMap<Aluno, ViewAlunoDto>().ReverseMap();
        }
    }
}