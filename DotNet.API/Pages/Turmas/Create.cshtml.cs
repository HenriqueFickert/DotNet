using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Application.Validators.Turma;
using DotNet.Domain.Core.Notification;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Turmas
{
    public class CreateModel : PageModel
    {
        private readonly ITurmaApplication _turmaApplication;
        private readonly INotifier _notifier;

        public PostTurmaDto Turma { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public CreateModel(ITurmaApplication turmaApplication, INotifier notifier)
        {
            _turmaApplication = turmaApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnPostAsync([Bind(Prefix = "Turma")] PostTurmaDto postTurmaDto)
        {
            Turma = postTurmaDto;

            if (!ModelState.IsValid)
                return Page();

            ValidationResult = new PostTurmaValidator(_turmaApplication).Validate(Turma);

            if (ValidationResult.IsValid)
            {
                ViewTurmaDto turma = await _turmaApplication.Post(Turma);

                if (_notifier.HasAnyError())
                    TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                else if (turma != null)
                    TempData["success"] = "Turma criada com sucesso.";

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