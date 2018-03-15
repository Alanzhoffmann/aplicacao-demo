using System;
using System.Collections.Generic;

namespace AplicacaoDemo.Dominio.Entidade
{
    public class Venda : EntidadeBase
    {
        private const decimal _valorFrete = 5;
        private const decimal _distanciaMaximaCalculoSimples = 100;

        public IList<ProdutoVenda> Produtos { get; set; }
        public DateTime DataVenda { get; set; }
        public int DistanciaQuilometros { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorTotal { get; set; }

        public Venda()
        {
            Produtos = new List<ProdutoVenda>();
            DataVenda = DateTime.Now;
        }

        public void CalcularValor()
        {
            decimal pesoTotal = 0;
            foreach (ProdutoVenda i in Produtos)
            {
                ValorVenda += i.Produto.Valor * i.Quantidade;
                pesoTotal += i.Produto.Peso * i.Quantidade;
            }

            ValorFrete = pesoTotal * _valorFrete;

            if (DistanciaQuilometros > _distanciaMaximaCalculoSimples)
            {
                ValorFrete *= (DistanciaQuilometros / _distanciaMaximaCalculoSimples);
            }

            ValorTotal = ValorFrete + ValorVenda;
        }
    }
}
