using DotNet.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Application.Dtos.Aluno
{
    public class PostAlunoDto
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O campo usuário é obrigatório.")]
        [StringLength(45, MinimumLength = 5, ErrorMessage = "O usuário precisa ter no mínimo 5 e no máximo 45 caracteres.")]
        public string Usuario { get; set; }

        [Display(Name = "Senha")]
        [PasswordValidation]
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "A senha precisa ter no mínimo 8 e no máximo 30 caracteres.")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "O campo confirmar senha é obrigatório.")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }
}