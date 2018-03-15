using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Interface.Repositorio;
using AplicacaoDemo.Dominio.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplicacaoDemo.Dominio.Servico
{
    public class ServicoBase<T> where T : EntidadeBase
    {
        public IRepositorioBase<T> _repositorioBase;

        public ServicoBase(IRepositorioBase<T> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task<T> RetornaPorId(long id)
        {
            return await _repositorioBase.RetornaPorId(id);
        }

        public async Task<IList<T>> RetornaPorId(params long[] ids)
        {
            return await _repositorioBase.RetornaPorId(ids);
        }

        public async Task<IList<T>> RetornaTodos()
        {
            return await _repositorioBase.RetornaTodos();
        }

        public async Task<ListaPaginada<T>> RetornaTodos(int pagina, int tamanhoPagina)
        {
            return await _repositorioBase.RetornaTodos(pagina, tamanhoPagina);
        }

        public async Task<T> Salvar(T entidade)
        {
            return await _repositorioBase.Salvar(entidade);
        }

        public async Task Salvar(params T[] entidades)
        {
            await _repositorioBase.Salvar(entidades);
        }

        public async Task<T> Excluir(T entidade)
        {
            return await _repositorioBase.Excluir(entidade);
        }
    }
}
