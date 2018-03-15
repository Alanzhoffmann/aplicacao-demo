using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Interface.Repositorio;

namespace AplicacaoDemo.Dominio.Servico
{
    public class ProdutoServico : ServicoBase<Produto>
    {
        private readonly IProdutoRepositorio _repositorio;

        public ProdutoServico(IProdutoRepositorio repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }
    }
}
