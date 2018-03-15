using System;
using AplicacaoDemo.Repositorio.Helper.Conexao;
using AplicacaoDemo.Repositorio.Helper.Enum;
using NHibernate;

namespace AplicacaoDemo.Repositorio.Helper
{
    class ConexaoNhibernate
    {
        private static Lazy<ISessionFactory> _lazy = new Lazy<ISessionFactory>();
        private static ISessionFactory _sessionFactory => _lazy.Value;

        private static BancoDados _bancoDados = BancoDados.MySql;
        private static bool _atualizarTabelas = false;

        private static ISessionFactory SessionFactory()
        {
            // TODO: Implementar outras conexões

            try
            {
                switch (_bancoDados)
                {
                    case BancoDados.MySql:
                        return ConexaoMySql.OpenSession(_atualizarTabelas);
                    case BancoDados.SqlServer:
                    case BancoDados.Oracle:
                    case BancoDados.Postgres:
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log da exceção de conexão
                return null;
            }
            finally
            {
                _atualizarTabelas = false;
            }
        }

        public static ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                _lazy = new Lazy<ISessionFactory>(SessionFactory);
            }

            return _sessionFactory.OpenSession();
        }

        public static void AtualizarTabelas()
        {
            _atualizarTabelas = true;
            _lazy = new Lazy<ISessionFactory>(SessionFactory);
        }
    }
}
