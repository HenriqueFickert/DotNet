namespace DotNet.Application.Dtos.AlunoTurma
{
    public class DeleteAlunoTurmaDto
    {
        public int Id { get; set; }
        public int TurmaId { get; set; }

        public DeleteAlunoTurmaDto()
        {
        }

        public DeleteAlunoTurmaDto(int id, int turmaId)
        {
            Id = id;
            TurmaId = turmaId;
        }
    }
}