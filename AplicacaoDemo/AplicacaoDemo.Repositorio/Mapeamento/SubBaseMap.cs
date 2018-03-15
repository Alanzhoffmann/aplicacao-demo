using AplicacaoDemo.Dominio.Entidade;
using FluentNHibernate.Mapping;

namespace AplicacaoDemo.Repositorio.Mapeamento
{
    public class SubBaseMap<T> : SubclassMap<T> where T : EntidadeBase
    {
        public SubBaseMap()
        {
            Table(typeof(T).Name.ToLower());
        }
    }
}
