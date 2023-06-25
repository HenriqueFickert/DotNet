using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Interfaces;
using DotNet.Application.Validators.Aluno;
using DotNet.Domain.Core.Notification;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Alunos
{
    public class CreateModel : PageModel
    {
        private readonly IAlunoApplication _alunoApplication;
        private readonly INotifier _notifier;

        public PostAlunoDto Aluno { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public CreateModel(IAlunoApplication alunoApplication, INotifier notifier)
        {
            _alunoApplication = alunoApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnPostAsync([Bind(Prefix = "Aluno")] PostAlunoDto postAlunoDto)
        {
            Aluno = postAlunoDto;
            if (!ModelState.IsValid)
                return Page();

            ValidationResult = new PostAlunoValidator().Validate(Aluno);

            if (ValidationResult.IsValid)
            {
                ViewAlunoDto aluno = await _alunoApplication.Post(Aluno);

                if (_notifier.HasAnyError())
                    TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                else if (aluno != null)
                    TempData["success"] = "Aluno criado com sucesso.";

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