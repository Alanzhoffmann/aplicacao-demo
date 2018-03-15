using AplicacaoDemo.Dominio.Entidade;

namespace AplicacaoDemo.Repositorio.Mapeamento
{
    public class ProdutoMap : BaseMap<Produto>
    {
        public ProdutoMap() : base()
        {
            Map(p => p.Peso);
            Map(p => p.Valor);
            Map(p => p.Descricao);
            Map(p => p.DataCadastro);
        }
    }
}
