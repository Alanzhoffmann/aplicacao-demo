using System;

namespace AplicacaoDemo.Dominio.Entidade
{
    public class Produto : EntidadeBase
    {
        public decimal Peso { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }

        public Produto()
        {
            DataCadastro = DateTime.Now;
        }
    }
}
