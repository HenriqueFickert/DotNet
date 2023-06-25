using DotNet.Domain.Enums;

namespace DotNet.Application.Dtos.Aluno
{
    public class ViewAlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public EStatus Status { get; set; }

        public ViewAlunoDto()
        { }

        public ViewAlunoDto(int id, EStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}