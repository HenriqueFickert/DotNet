using DotNet.Domain.Entities.Base;

namespace DotNet.Domain.Pagination
{
    public class EntityPaged<T> where T : EntityBase
    {
        public T Entity { get; set; }

        public int PaginaAtual { get; set; }

        public int TotalPaginas { get; set; }

        public int ResultadosExibidosPagina { get; set; }

        public int ContagemTotalResultados { get; set; }

        public bool ExistePaginaAnterior => PaginaAtual > 1 && TotalPaginas > 1;

        public bool ExistePaginaPosterior => PaginaAtual < TotalPaginas;

        public EntityPaged(T entity, int count, int pageNumber, int pageSize)
        {
            ContagemTotalResultados = count;
            ResultadosExibidosPagina = pageSize;
            PaginaAtual = pageNumber;
            TotalPaginas = (int)Math.Ceiling(count / (double)pageSize);
            Entity = entity;
        }
    }
}