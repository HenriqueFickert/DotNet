using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using FluentValidation;

namespace DotNet.Application.Validators.Turma
{
    public class PutTurmaValidator : AbstractValidator<PutTurmaDto>
    {
        private readonly ITurmaApplication _turmaApplication;

        public PutTurmaValidator(ITurmaApplication turmaApplication)
        {
            _turmaApplication = turmaApplication;

            RuleFor(x => x.Nome)
              .NotNull()
              .WithMessage("O campo nome não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo nome não pode ser vazio.")

              .MinimumLength(5)
              .WithMessage("O nome da turma precisa conter pelo menos 5 caracteres.")

              .MaximumLength(45)
              .WithMessage("O nome da turma pode conter no máximo 45 caracteres.");

            RuleFor(x => x.CursoId)
              .NotNull()
              .WithMessage("O campo id de curso não pode ser nulo.")

              .NotEmpty()
              .WithMessage("O campo id de curso não pode ser vazio.");

            RuleFor(x => x.Ano)
                .NotNull()
                .WithMessage("O campo ano não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O campo ano não pode ser vazio.");

            RuleFor(dto => dto.Ano)
                .Must((dto, cancellation) =>
                {
                    return YearValidator(dto.Ano);
                }).WithMessage("O ano da turma deve ser igual ou posterior ao ano atual.");

            RuleFor(dto => dto)
                .Must((dto, cancellation) =>
                {
                    return !TurmaNameValidator(dto);
                }).WithMessage("Já existe uma turma cadastrada com este nome.");
        }

        private bool TurmaNameValidator(PutTurmaDto putTurmaDto)
        {
            return _turmaApplication.TurmaNameValidator(putTurmaDto.Nome, putTurmaDto.Id);
        }

        private bool YearValidator(int year)
        {
            return year >= DateTime.Now.Year;
        }
    }
}