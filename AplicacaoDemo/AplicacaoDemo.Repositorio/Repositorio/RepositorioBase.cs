using System.Collections.Generic;
using System.Threading.Tasks;
using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Interface.Repositorio;
using AplicacaoDemo.Dominio.Utils;
using AplicacaoDemo.Repositorio.Helper;
using NHibernate;

namespace AplicacaoDemo.Repositorio.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        public virtual async Task<T> Excluir(T entidade)
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(entidade);
                    await transaction.CommitAsync();
                }
            }
            return entidade;
        }

        public virtual async Task<T> RetornaPorId(long id)
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                return await session.GetAsync<T>(id);
            }
        }

        public virtual async Task<IList<T>> RetornaPorId(params long[] ids)
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                return await session.QueryOver<T>()
                    .WhereRestrictionOn(t => t.Id).IsIn(ids)
                    .ListAsync();
            }
        }

        public virtual async Task<IList<T>> RetornaTodos()
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                return await session.QueryOver<T>().ListAsync();
            }
        }

        public virtual async Task<ListaPaginada<T>> RetornaTodos(int pagina, int tamanhoPagina)
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                return await session.QueryOver<T>().PaginadoAsync(pagina, tamanhoPagina);
            }
        }

        public virtual async Task<T> Salvar(T entidade)
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    await session.SaveOrUpdateAsync(entidade);
                    await transaction.CommitAsync();
                }
            }
            return entidade;
        }

        public virtual async Task Salvar(params T[] entidades)
        {
            using (ISession session = ConexaoNhibernate.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (T i in entidades)
                    {
                        await session.SaveOrUpdateAsync(i);
                    }
                    await transaction.CommitAsync();
                }
            }
        }
    }

    public static class RepositoryExtensions
    {
        public static ListaPaginada<T> Paginado<T>(this IQueryOver<T, T> query, int pagina, int tamanhoPagina) where T : class
        {
            IFutureValue<long> count = MontarQueryPaginado(query, pagina, tamanhoPagina);

            return new ListaPaginada<T>(query.Future(), count.Value, tamanhoPagina);
        }

        public static ListaPaginada<object> PaginadoObject<T>(this IQueryOver<T, T> query, int pagina, int tamanhoPagina) where T : class
        {
            IFutureValue<long> count = MontarQueryPaginado(query, pagina, tamanhoPagina);

            return new ListaPaginada<object>(query.Future<object>(), count.Value, tamanhoPagina);
        }

        public static async Task<ListaPaginada<T>> PaginadoAsync<T>(this IQueryOver<T, T> query, int pagina, int tamanhoPagina) where T : class
        {
            IFutureValue<long> count = MontarQueryPaginado(query, pagina, tamanhoPagina);

            return new ListaPaginada<T>(await query.ListAsync(), count.Value, tamanhoPagina);
        }

        public static async Task<ListaPaginada<object>> PaginadoObjectAsync<T>(this IQueryOver<T, T> query, int pagina, int tamanhoPagina) where T : class
        {
            IFutureValue<long> count = MontarQueryPaginado(query, pagina, tamanhoPagina);

            return await Task.FromResult(new ListaPaginada<object>(await query.ListAsync<object>(), count.Value, tamanhoPagina));
        }

        private static IFutureValue<long> MontarQueryPaginado<T>(IQueryOver<T, T> query, int pagina, int tamanhoPagina) where T : class
        {
            IFutureValue<long> count = query.ToRowCountInt64Query().FutureValue<long>();

            if (pagina > 0 && tamanhoPagina > 0)
            {
                query.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina);
            }

            return count;
        }
    }
}
