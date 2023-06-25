using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Alunos
{
    public class EditModel : PageModel
    {
        private readonly IAlunoApplication _alunoApplication;

        [BindProperty]
        public PutAlunoDto Aluno { get; set; }

        private readonly INotifier _notifier;

        public EditModel(IAlunoApplication alunoApplication, INotifier notifier)
        {
            _alunoApplication = alunoApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (id == 0)
                return RedirectToPage("Index");

            Aluno = await _alunoApplication.GetPutData(id);

            if (Aluno == null)
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ViewAlunoDto aluno = await _alunoApplication.Put(Aluno);

                if (_notifier.HasAnyError())
                    TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                else if (aluno != null)
                    TempData["success"] = "Aluno atualizado com sucesso.";

                _notifier.ClearErrors();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}