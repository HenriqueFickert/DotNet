using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Alunos
{
    public class DeleteModel : PageModel
    {
        private readonly IAlunoApplication _alunoApplication;
        private readonly INotifier _notifier;

        [BindProperty]
        public ViewAlunoDto Aluno { get; set; }

        public DeleteModel(IAlunoApplication alunoApplication, INotifier notifier)
        {
            _alunoApplication = alunoApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id == 0)
                return RedirectToPage("Index");

            Aluno = await _alunoApplication.GetById(id);

            if (Aluno == null)
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool success = await _alunoApplication.Delete(Aluno.Id);

            if (_notifier.HasAnyError())
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
            else if (success)
                TempData["success"] = "Aluno excluído com sucesso.";

            _notifier.ClearErrors();
            return RedirectToPage("Index");
        }
    }
}