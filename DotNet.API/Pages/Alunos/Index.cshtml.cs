using DotNet.Application.Dtos.Aluno;
using DotNet.Application.Dtos.Pagination;
using DotNet.Application.Interfaces;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages.Alunos
{
    public class IndexModel : PageModel
    {
        private readonly IAlunoApplication _alunoApplication;
        private readonly INotifier _notifier;

        public int PageNumber { get; set; } = 1;
        public readonly int PageSize = 10;
        public IEnumerable<ViewAlunoDto> Alunos { get; set; }
        public ViewPaginationDataDto<Aluno> TableData { get; set; } = new();

        public IndexModel(IAlunoApplication alunoApplication, INotifier notifier)
        {
            _alunoApplication = alunoApplication;
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
            bool result = await _alunoApplication.PutStatus(objectId, (EStatus)Enum.Parse(typeof(EStatus), status));

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
            ViewPaginationDto<Aluno, ViewAlunoDto> pagedList = await _alunoApplication.GetAllPaginated(PageNumber, PageSize);

            if (_notifier.HasAnyError())
            {
                TempData["error"] = _notifier.GetAllNotifications().FirstOrDefault().Message;
                _notifier.ClearErrors();
                return RedirectToPage("../Index");
            }

            TableData = pagedList.Dados;
            Alunos = pagedList.Pagina;
            return Page();
        }
    }
}