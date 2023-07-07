using DotNet.Application.Dtos.Pagination;
using DotNet.Application.Dtos.Turma;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Turmas
{
    public class IndexModel : PageModel
    {
        private readonly ITurmaApplication _turmaApplication;
        private readonly INotifier _notifier;

        public int PageNumber { get; set; } = 1;
        public readonly int PageSize = 10;
        public IEnumerable<ViewTurmaDto> Turmas { get; set; }
        public ViewPaginationDataDto<Turma> TableData { get; set; } = new();

        public IndexModel(ITurmaApplication turmaApplication, INotifier notifier)
        {
            _turmaApplication = turmaApplication;
            _notifier = notifier;
        }

        public async Task<IActionResult> OnGetAsync(int? pageNumber)
        {
            if (pageNumber.HasValue && pageNumber > 0)
                PageNumber = pageNumber.Value;

            return await GetData();
        }

        public async Task<IActionResult> OnPost(int objectId, string status, int? pageNumber)
        {
            bool result = await _turmaApplication.PutStatus(objectId, (EStatus)Enum.Parse(typeof(EStatus), status));

            if (_notifier.HasAnyError())
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
            else if (result)
                TempData["success"] = "Status alterado com sucesso.";

            if (pageNumber.HasValue)
                PageNumber = pageNumber.Value;

            return await GetData();
        }

        private async Task<IActionResult> GetData()
        {
            ViewPaginationDto<Turma, ViewTurmaDto> pagedList = await _turmaApplication.GetAllPaginated(PageNumber, PageSize);

            if (_notifier.HasAnyError())
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                _notifier.ClearErrors();
                return RedirectToPage("../Index");
            }

            TableData = pagedList.Dados;
            Turmas = pagedList.Pagina;
            return Page();
        }
    }
}