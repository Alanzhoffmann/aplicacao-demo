using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplicacaoDemo.Dominio.Interface.Repositorio
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        Task<T> RetornaPorId(long id);
        Task<IList<T>> RetornaPorId(params long[] ids);

        Task<IList<T>> RetornaTodos();
        Task<ListaPaginada<T>> RetornaTodos(int pagina, int tamanhoPagina);

        Task<T> Salvar(T entidade);
        Task Salvar(params T[] entidades);

        Task<T> Excluir(T entidade);

    }
}
