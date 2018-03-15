using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace AplicacaoDemo.Repositorio.Helper.Conexao
{
    class ConexaoMySql
    {
        internal static ISessionFactory OpenSession(bool atualizarTabelas = false)
        {
            return ConexaoBase.OpenSession((ConfiguracaoConexao configuracao) =>
                MySQLConfiguration.Standard.ConnectionString(
                    $"Server={configuracao.Endereco};" +
                    $"Port={(configuracao.Porta == 0 ? 3306 : configuracao.Porta)};" +
                    $"Database={configuracao.NomeBanco};" +
                    $"Uid={configuracao.Usuario};" +
                    $"Pwd={configuracao.Senha};"), atualizarTabelas);
        }
    }
}
