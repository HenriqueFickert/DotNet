using DotNet.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Application.Dtos.Turma
{
    public class PostTurmaDto
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(45, MinimumLength = 5, ErrorMessage = "O nome da turma precisa ter no mínimo 5 e no máximo 45 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Curso Id")]
        [Required(ErrorMessage = "O campo id de curso é obrigatório.")]
        public int CursoId { get; set; }

        [Display(Name = "Ano")]
        [YearValidation]
        [Required(ErrorMessage = "O campo ano é obrigatório.")]
        public int Ano { get; set; }
    }
}