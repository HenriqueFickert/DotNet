using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Dtos.AlunoTurma;
using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Application.Validators.AlunoTurma;
using DotNet.Domain.Core.Notification;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.RelacionarTurmas
{
    public class CreateModel : PageModel
    {
        private readonly ITurmaApplication _turmaApplication;
        private readonly IAlunoApplication _alunoApplication;
        private readonly INotifier _notifier;

        public IEnumerable<ViewTurmaDto> Turmas { get; set; }
        public IEnumerable<ViewAlunoDto> Alunos { get; set; }

        private PostAlunoTurmaDto AlunoTurma { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public CreateModel(ITurmaApplication turmaApplication, IAlunoApplication alunoApplication, INotifier notifier)
        {
            _turmaApplication = turmaApplication;
            _alunoApplication = alunoApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await GetData();
        }

        public async Task<IActionResult> OnPost([Bind(Prefix = "AlunoTurma")] PostAlunoTurmaDto postAlunoTurmaDto)
        {
            AlunoTurma = postAlunoTurmaDto;

            ValidationResult = new PostAlunoTurmaValidator(_alunoApplication).Validate(AlunoTurma);

            if (ValidationResult.IsValid)
            {
                bool result = await _alunoApplication.InsertAlunoTurma(AlunoTurma);

                if (_notifier.HasAnyError())
                    TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                else if (result)
                    TempData["success"] = "Aluno criado com sucesso.";

                _notifier.ClearErrors();
                return RedirectToPage("Index");
            }
            else
            {
                foreach (var error in ValidationResult.Errors)
                    TempData["error"] = error.ErrorMessage;

                return await GetData();
            }
        }

        private async Task<IActionResult> GetData()
        {
            Alunos = await _alunoApplication.GetAll();
            Turmas = await _turmaApplication.GetAll();

            if (_notifier.HasAnyError())
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                _notifier.ClearErrors();
                return RedirectToPage("../Index");
            }

            return Page();
        }
    }
}