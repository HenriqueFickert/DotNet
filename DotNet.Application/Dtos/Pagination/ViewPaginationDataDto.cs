using DotNet.Domain.Entities.Base;
using DotNet.Domain.Pagination;

namespace DotNet.Application.Dtos.Pagination
{
    public class ViewPaginationDataDto<T> where T : EntityBase
    {
        public int PaginaAtual { get; private set; }

        public int TotalPaginas { get; private set; }

        public int ResultadosExibidosPagina { get; private set; }

        public int ContagemTotalResultados { get; private set; }

        public bool ExistePaginaAnterior { get; private set; }

        public bool ExistePaginaPosterior { get; private set; }

        public ViewPaginationDataDto()
        {
        }

        public ViewPaginationDataDto(PagedList<T> pagedList)
        {
            ContagemTotalResultados = pagedList.ContagemTotalResultados;

            ResultadosExibidosPagina = pagedList.ResultadosExibidosPagina;

            PaginaAtual = pagedList.PaginaAtual;

            TotalPaginas = pagedList.TotalPaginas;

            ExistePaginaPosterior = pagedList.ExistePaginaPosterior;

            ExistePaginaAnterior = pagedList.ExistePaginaAnterior;
        }

        public ViewPaginationDataDto(EntityPaged<T> entityPaged)
        {
            ContagemTotalResultados = entityPaged.ContagemTotalResultados;

            ResultadosExibidosPagina = entityPaged.ResultadosExibidosPagina;

            PaginaAtual = entityPaged.PaginaAtual;

            TotalPaginas = entityPaged.TotalPaginas;

            ExistePaginaPosterior = entityPaged.ExistePaginaPosterior;

            ExistePaginaAnterior = entityPaged.ExistePaginaAnterior;
        }
    }
}