using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Application.Validators.Turma;
using DotNet.Domain.Core.Notification;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Turmas
{
    public class EditModel : PageModel
    {
        private readonly ITurmaApplication _turmaApplication;
        private readonly INotifier _notifier;

        public PutTurmaDto Turma { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public EditModel(ITurmaApplication turmaApplication, INotifier notifier)
        {
            _turmaApplication = turmaApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id == 0)
                return RedirectToPage("Index");

            Turma = await _turmaApplication.GetPutData(id);

            if (Turma == null)
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([Bind(Prefix = "Turma")] PutTurmaDto putTurmaDto)
        {
            Turma = putTurmaDto;

            if (!ModelState.IsValid)
                return Page();

            ValidationResult = new PutTurmaValidator(_turmaApplication).Validate(Turma);

            if (ValidationResult.IsValid)
            {
                ViewTurmaDto aluno = await _turmaApplication.Put(Turma);

                if (_notifier.HasAnyError())
                    TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                else if (aluno != null)
                    TempData["success"] = "Turma atualizada com sucesso.";

                _notifier.ClearErrors();
                return RedirectToPage("Index");
            }
            else
            {
                foreach (var error in ValidationResult.Errors)
                    TempData["error"] = error.ErrorMessage;

                return Page();
            }
        }
    }
}