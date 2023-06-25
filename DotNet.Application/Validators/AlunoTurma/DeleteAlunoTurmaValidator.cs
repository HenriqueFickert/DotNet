using DotNet.Application.Dtos.AlunoTurma;
using DotNet.Application.Interfaces;
using FluentValidation;

namespace DotNet.Application.Validators.AlunoTurma
{
    public class DeleteAlunoTurmaValidator : AbstractValidator<DeleteAlunoTurmaDto>
    {
        private readonly IAlunoApplication _alunoApplication;

        public DeleteAlunoTurmaValidator(IAlunoApplication alunoApplication)
        {
            _alunoApplication = alunoApplication;

            RuleFor(x => x.Id)
              .NotNull()
              .WithMessage("O campo id de turma não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo id de turma não pode ser vazio.");

            RuleFor(x => x.TurmaId)
              .NotNull()
              .WithMessage("O campo id de aluno não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo id de aluno não pode ser vazio.");

            RuleFor(dto => dto)
                .Must((dto, cancellation) =>
                {
                    return TurmaNameValidator(dto);
                }).WithMessage("O aluno selecionado já está associado a esta turma.");
        }

        private bool TurmaNameValidator(DeleteAlunoTurmaDto deleteAlunoTurmaDto)
        {
            return _alunoApplication.AlunoTurmaValidator(deleteAlunoTurmaDto.Id, deleteAlunoTurmaDto.TurmaId);
        }
    }
}