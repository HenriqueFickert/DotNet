using DotNet.Domain.Enums;

namespace DotNet.Application.Dtos.Turma
{
    public class ViewTurmaDto
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public EStatus Status { get; set; }

        public ViewTurmaDto()
        { }

        public ViewTurmaDto(int id, EStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}