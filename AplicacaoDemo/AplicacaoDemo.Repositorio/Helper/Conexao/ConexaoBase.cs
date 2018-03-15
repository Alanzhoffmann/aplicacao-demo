using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;

namespace AplicacaoDemo.Repositorio.Helper.Conexao
{
    public class ConexaoBase
    {
        public static ISessionFactory OpenSession(Func<ConfiguracaoConexao, IPersistenceConfigurer> configuracao, bool atualizarTabelas = false)
        {
            if (configuracao == null)
            {
                throw new Exception("Configuração incorreta de conexão");
            }

            var configuracaoBanco = new ConfiguracaoConexao
            {

            };

            FluentConfiguration fluentConfiguration = Fluently.Configure()
                .Database(configuracao.Invoke(configuracaoBanco))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ConexaoNhibernate>())
                .ExposeConfiguration(c =>
                {
                    c.Properties.Add(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
                    c.Properties.Add(NHibernate.Cfg.Environment.DefaultBatchFetchSize, 15.ToString());
                });

            if (atualizarTabelas)
            {
                //Atualiza as tabelas do Banco de Dados
                fluentConfiguration.ExposeConfiguration(cfg =>
                {
                    new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(true, true);
                });
            }

            return fluentConfiguration.BuildSessionFactory();
        }
    }

    public class ConfiguracaoConexao
    {
        public string Endereco { get; set; }
        public string NomeBanco { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
