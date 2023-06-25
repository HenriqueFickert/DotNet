using DotNet.Domain.Entities.Base;

namespace DotNet.Domain.Entities
{
    public class Aluno : EntityBase
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public ICollection<Turma> Turmas { get; set; }
    }
}