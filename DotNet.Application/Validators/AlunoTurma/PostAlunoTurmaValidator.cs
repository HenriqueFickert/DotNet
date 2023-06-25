using DotNet.Application.Dtos.AlunoTurma;
using DotNet.Application.Interfaces;
using FluentValidation;

namespace DotNet.Application.Validators.AlunoTurma
{
    public class PostAlunoTurmaValidator : AbstractValidator<PostAlunoTurmaDto>
    {
        private readonly IAlunoApplication _alunoApplication;

        public PostAlunoTurmaValidator(IAlunoApplication alunoApplication)
        {
            _alunoApplication = alunoApplication;

            RuleFor(x => x.TurmaSelecionadaId)
              .NotNull()
              .WithMessage("O campo id de turma não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo id de turma não pode ser vazio.");

            RuleFor(x => x.AlunoSelecionadoId)
              .NotNull()
              .WithMessage("O campo id de aluno não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo id de aluno não pode ser vazio.");

            RuleFor(dto => dto)
                .Must((dto, cancellation) =>
                {
                    return !TurmaNameValidator(dto);
                }).WithMessage("O aluno selecionado já está associado a esta turma.");
        }

        private bool TurmaNameValidator(PostAlunoTurmaDto postAlunoTurmaDto)
        {
            return _alunoApplication.AlunoTurmaValidator(postAlunoTurmaDto.AlunoSelecionadoId, postAlunoTurmaDto.TurmaSelecionadaId);
        }
    }
}