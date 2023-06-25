using DotNet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }

        public EntityBase()
        { }

        public void ChangeStatus()
        {
            if (Status == EStatus.Ativo.ToString())
                Status = EStatus.Inativo.ToString();
            else
                Status = EStatus.Ativo.ToString();
        }
    }
}