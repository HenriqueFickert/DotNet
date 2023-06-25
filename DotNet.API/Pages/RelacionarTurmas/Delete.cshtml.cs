using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Dtos.AlunoTurma;
using DotNet.Application.Interfaces;
using DotNet.Application.Validators.AlunoTurma;
using DotNet.Domain.Core.Notification;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.RelacionarTurmas
{
    public class DeleteModel : PageModel
    {
        private readonly IAlunoApplication _alunoApplication;
        private readonly INotifier _notifier;

        [BindProperty]
        public ViewAlunoDto Aluno { get; set; }

        public int TurmaId { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public DeleteModel(IAlunoApplication alunoApplication, INotifier notifier)
        {
            _alunoApplication = alunoApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGet(int id, int turmaId)
        {
            if (id == 0 || turmaId == 0)
                return RedirectToPage("Index");

            TurmaId = turmaId;
            Aluno = await _alunoApplication.GetById(id);

            if (Aluno == null)
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int turmaId)
        {
            ValidationResult = new DeleteAlunoTurmaValidator(_alunoApplication).Validate(new DeleteAlunoTurmaDto(Aluno.Id, turmaId));

            if (ValidationResult.IsValid)
            {
                TurmaId = turmaId;
                bool success = await _alunoApplication.DeleteAlunoTurma(Aluno.Id, TurmaId);

                if (_notifier.HasAnyError())
                    TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                else if (success)
                    TempData["success"] = "Aluno excluído com sucesso.";

                _notifier.ClearErrors();
            }
            else
            {
                foreach (var error in ValidationResult.Errors)
                    TempData["error"] = error.ErrorMessage;
            }

            return RedirectToPage("Index");
        }
    }
}