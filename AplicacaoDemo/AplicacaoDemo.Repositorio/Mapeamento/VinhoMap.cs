using AplicacaoDemo.Dominio.Entidade;

namespace AplicacaoDemo.Repositorio.Mapeamento
{
    public class VinhoMap : SubBaseMap<Vinho>
    {
        public VinhoMap() : base()
        {
            Map(v => v.TipoVinho).CustomType<Dominio.Enumerador.TipoVinho>();
        }
    }
}
