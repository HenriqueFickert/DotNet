using DotNet.Application.Dtos.Aluno;
using FluentValidation;

namespace DotNet.Application.Validators.Aluno
{
    public class PostAlunoValidator : AbstractValidator<PostAlunoDto>
    {
        public PostAlunoValidator()
        {
            RuleFor(x => x.Nome)
              .NotNull()
              .WithMessage("O campo nome não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo nome não pode ser vazio.");

            RuleFor(x => x.Usuario)
              .NotNull()
              .WithMessage("O campo id de curso não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo id de curso não pode ser vazio.")

              .MinimumLength(5)
              .WithMessage("O nome da turma precisa conter pelo menos 5 caracteres.")

              .MaximumLength(45)
              .WithMessage("O nome da turma pode conter no máximo 45 caracteres.");

            RuleFor(x => x.Senha)
                .NotNull()
                .WithMessage("O campo ano não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O campo ano não pode ser vazio.")

                .MinimumLength(8)
                .WithMessage("O nome da turma precisa conter pelo menos 8 caracteres.")

                .MaximumLength(30)
                .WithMessage("O nome da turma pode conter no máximo 30 caracteres.");

            RuleFor(dto => dto)
                .Must((dto, cancellation) =>
                {
                    return PasswordValidator(dto);
                }).WithMessage("O ano da turma deve ser igual ou posterior ao ano atual.");
        }

        private bool PasswordValidator(PostAlunoDto postAlunoDto)
        {
            return postAlunoDto.Senha == postAlunoDto.ConfirmarSenha;
        }
    }
}