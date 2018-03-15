using AplicacaoDemo.Dominio.Enumerador;

namespace AplicacaoDemo.Dominio.Entidade
{
    public class Vinho : Produto
    {
        public TipoVinho TipoVinho { get; set; }

        public Vinho() : base
        {

        }
    }
}
