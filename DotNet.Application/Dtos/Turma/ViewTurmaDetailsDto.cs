using DotNet.Application.Dtos.Aluno;
using DotNet.Domain.Enums;

namespace DotNet.Application.Dtos.Turma
{
    public class ViewTurmaDetailsDto
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public EStatus Status { get; set; }
        public List<ViewAlunoDto> Alunos { get; set; }
    }
}