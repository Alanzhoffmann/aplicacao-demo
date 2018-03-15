using AplicacaoDemo.Dominio.Entidade;
using FluentNHibernate.Mapping;

namespace AplicacaoDemo.Repositorio.Mapeamento
{
    public class ProdutoVendaMap : ClassMap<ProdutoVenda>
    {
        public ProdutoVendaMap()
        {
            Table(typeof(ProdutoVenda).Name.ToLower());

            Map(p => p.Produto);
            Map(p => p.Quantidade);
        }
    }
}
