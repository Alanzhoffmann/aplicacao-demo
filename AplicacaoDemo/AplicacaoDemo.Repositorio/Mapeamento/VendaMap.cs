using AplicacaoDemo.Dominio.Entidade;

namespace AplicacaoDemo.Repositorio.Mapeamento
{
    public class VendaMap : BaseMap<Venda>
    {
        public VendaMap() : base()
        {
            Map(v => v.DataVenda);
            Map(v => v.DistanciaQuilometros);
            Map(v => v.ValorVenda);
            Map(v => v.ValorFrete);
            Map(v => v.ValorTotal);

            HasMany(v => v.Produtos).Cascade.All();
        }
    }
}
