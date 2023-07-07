using DotNet.Application.Dtos.Pagination;
using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.RelacionarTurmas
{
    public class TurmaAlunosModel : PageModel
    {
        private readonly ITurmaApplication _turmaApplication;
        private readonly INotifier _notifier;

        public int PageNumber { get; set; } = 1;
        public readonly int PageSize = 10;

        public ViewTurmaDetailsDto Turma { get; set; }
        public ViewPaginationDataDto<Turma> TableData { get; set; } = new();

        public TurmaAlunosModel(ITurmaApplication turmaApplication, INotifier notifier)
        {
            _turmaApplication = turmaApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGetAsync(int id, int? pageNumber)
        {
            if (pageNumber.HasValue && pageNumber > 0)
                PageNumber = pageNumber.Value;

            ViewEntityPaginationDto<Turma, ViewTurmaDetailsDto> entityPaged = await _turmaApplication.GetDetails(id, PageNumber, PageSize);

            if (_notifier.HasAnyError())
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                _notifier.ClearErrors();
                return RedirectToPage("../Index");
            }

            TableData = entityPaged.Dados;
            Turma = entityPaged.View;
            return Page();
        }
    }
}