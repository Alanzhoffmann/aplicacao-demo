using System.Collections.Generic;

namespace AplicacaoDemo.App.Models
{
    public class VendaViewModel
    {
        public IList<VendaProdutoViewModel> Produtos { get; set; }
        public int Distancia { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class VendaProdutoViewModel
    {
        public int Quantidade { get; set; }
        public long IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
    }

    public class VendaListaViewModel
    {
        public long Id { get; set; }
        public string DataVenda { get; set; }
        public int Distancia { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
