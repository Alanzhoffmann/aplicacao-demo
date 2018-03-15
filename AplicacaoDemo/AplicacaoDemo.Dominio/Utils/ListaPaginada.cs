using System.Collections.Generic;

namespace AplicacaoDemo.Dominio.Utils
{
    public class ListaPaginada<T> : List<T>
    {
        public long TotalCount { get; set; }
        public long PageSize { get; set; }

        public long PageCount => TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1);

        public ListaPaginada(IEnumerable<T> lista) : base(lista)
        {
            TotalCount = Count;
        }

        public ListaPaginada(IEnumerable<T> lista, long quantidadeLinhas) : this(lista)
        {
            TotalCount = quantidadeLinhas;
        }

        public ListaPaginada(IEnumerable<T> lista, long quantidadeLinhas, long tamanhoPagina) : this(lista, quantidadeLinhas)
        {
            PageSize = tamanhoPagina;
        }
    }
}
