using DotNet.Domain.Entities.Base;
using DotNet.Domain.Pagination;

namespace DotNet.Application.Dtos.Pagination
{
    public class ViewEntityPaginationDto<TEntity, TView> where TEntity : EntityBase where TView : class
    {
        public TView View { get; set; }

        public ViewPaginationDataDto<TEntity> Dados { get; set; }

        public ViewEntityPaginationDto(EntityPaged<TEntity> entityPaged, TView view)
        {
            View = view;
            Dados = new ViewPaginationDataDto<TEntity>(entityPaged);
        }
    }
}