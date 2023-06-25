using DotNet.Domain.Entities.Base;

namespace DotNet.Domain.Entities
{
    public class Turma : EntityBase
    {
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }

        public ICollection<Aluno> Alunos { get; set; }

        public Turma()
        {
            Alunos = new List<Aluno>();
        }
    }
}