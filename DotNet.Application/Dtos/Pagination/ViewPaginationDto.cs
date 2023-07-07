using DotNet.Domain.Entities.Base;
using DotNet.Domain.Pagination;

namespace DotNet.Application.Dtos.Pagination
{
    public class ViewPaginationDto<TEntity, TView> where TEntity : EntityBase where TView : class
    {
        public ICollection<TView> Pagina { get; set; }

        public ViewPaginationDataDto<TEntity> Dados;

        public ViewPaginationDto(PagedList<TEntity> pagedList, List<TView> list)
        {
            Pagina = list;
            Dados = new ViewPaginationDataDto<TEntity>(pagedList);
        }
    }
}