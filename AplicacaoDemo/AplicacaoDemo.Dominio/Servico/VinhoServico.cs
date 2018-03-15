using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Interface.Repositorio;

namespace AplicacaoDemo.Dominio.Servico
{
    public class VinhoServico : ServicoBase<Vinho>
    {
        private readonly IVinhoRepositorio _repositorio;

        public VinhoServico(IVinhoRepositorio repositorio) : base(repositorio)
        {
            _repositorio = repositorio;
        }
    }
}
