using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Turmas
{
    public class DeleteModel : PageModel
    {
        private readonly ITurmaApplication _turmaApplication;
        private readonly INotifier _notifier;

        [BindProperty]
        public ViewTurmaDto Turma { get; set; }

        public DeleteModel(ITurmaApplication turmaApplication, INotifier notifier)
        {
            _turmaApplication = turmaApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id == 0)
                return RedirectToPage("Index");

            Turma = await _turmaApplication.GetById(id);

            if (Turma == null)
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool success = await _turmaApplication.Delete(Turma.Id);

            if (_notifier.HasAnyError())
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
            else if (success)
                TempData["success"] = "Turma excluída com sucesso.";

            _notifier.ClearErrors();
            return RedirectToPage("Index");
        }
    }
}