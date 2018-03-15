using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Interface.Repositorio;

namespace AplicacaoDemo.Dominio.Servico
{
    public class VendaServico : ServicoBase<Venda>
    {
        private readonly IVendaRepositorio _repositorio;

        public VendaServico(IVendaRepositorio repositorio) : base(repositorio)
        {
            _repositorioBase = repositorio;
        }
    }
}
