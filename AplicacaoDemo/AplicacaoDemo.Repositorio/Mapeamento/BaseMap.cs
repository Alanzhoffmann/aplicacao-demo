using AplicacaoDemo.Dominio.Entidade;
using FluentNHibernate.Mapping;

namespace AplicacaoDemo.Repositorio.Mapeamento
{
    public class BaseMap<T> : ClassMap<T> where T : EntidadeBase
    {
        public BaseMap()
        {
            Table(typeof(T).Name.ToLower());
            Id(x => x.Id);
        }
    }
}
