using AutoMapper;
using DotNet.Application.Dtos.Aluno;
using DotNet.Domain.Entities;

namespace DotNet.Application.Mappings
{
    public class AlunoMappingProfile : Profile
    {
        public AlunoMappingProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<Aluno, ViewAlunoDto>().ReverseMap();
            CreateMap<Aluno, PostAlunoDto>().ReverseMap();
            CreateMap<Aluno, PutAlunoDto>().ReverseMap();
        }
    }
}