using AplicacaoDemo.App.Models;
using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Servico;
using AplicacaoDemo.Dominio.Utils;
using AplicacaoDemo.Repositorio.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacaoDemo.App.Controllers
{
    public class VendaController : Controller
    {
        VendaServico _vendaServico = new VendaServico(new VendaRepositorio());
        ProdutoServico _produtoServico = new ProdutoServico(new ProdutoRepositorio());

        public async Task<JsonResult> RetornaTodos(int pagina, int tamanhoPagina)
        {
            ListaPaginada<Venda> lista = await _vendaServico.RetornaTodos(pagina, tamanhoPagina);

            var listaModel = new List<VendaListaViewModel>();
            foreach (Venda i in lista)
            {
                listaModel.Add(new VendaListaViewModel()
                {
                    Id = i.Id,
                    DataVenda = i.DataVenda.ToString("dd/MM/yyyy"),
                    Distancia = i.DistanciaQuilometros,
                    ValorTotal = i.ValorTotal
                });
            }

            return Json(new { lista = listaModel, count = lista.TotalCount, pageCount = lista.PageCount });
        }

        public async Task<JsonResult> CalcularVenda(VendaViewModel model)
        {
            try
            {
                Venda venda = await MontarObjetoVenda(model, calcularVenda: true);

                var resultadoCalculo = new VendaViewModel
                {
                    Distancia = venda.DistanciaQuilometros,
                    ValorTotal = venda.ValorTotal,
                    ValorFrete = venda.ValorFrete,
                    ValorVenda = venda.ValorVenda
                };

                foreach (ProdutoVenda i in venda.Produtos)
                {
                    resultadoCalculo.Produtos.Add(new VendaProdutoViewModel()
                    {
                        IdProduto = i.Produto.Id,
                        DescricaoProduto = i.Produto.Descricao,
                        Quantidade = i.Quantidade
                    });
                }

                return Json(new { isValid = true, resultadoCalculo });
            }
            catch
            {
                // TODO: Log de exceções
                return Json(new { isValid = false, message = "Ocorreu um erro com a sua requisição. Tente novamente mais tarde" });
            }
        }

        private async Task<Venda> MontarObjetoVenda(VendaViewModel model, bool calcularVenda = false)
        {
            var venda = new Venda()
            {
                DistanciaQuilometros = model.Distancia
            };

            IList<Produto> produtos = await _produtoServico.RetornaPorId(model.Produtos.Select(p => p.IdProduto).ToArray());

            foreach (VendaProdutoViewModel i in model.Produtos)
            {
                Produto produto = produtos.FirstOrDefault(p => p.Id == i.IdProduto);
                if (produto != null)
                {
                    venda.Produtos.Add(new ProdutoVenda
                    {
                        Produto = produto,
                        Quantidade = i.Quantidade
                    });
                }
            }

            if (calcularVenda)
            {
                venda.CalcularValor();
            }
            return venda;
        }

        public async Task<JsonResult> SalvarVenda(VendaViewModel model)
        {
            try
            {
                Venda venda = await MontarObjetoVenda(model, calcularVenda: true);

                await _vendaServico.Salvar(venda);

                return Json(new { isValid = true, message = "Seu pedido foi efetuado com sucesso" });

            }
            catch
            {
                return Json(new { isValid = false, message = "Ocorreu um erro com a sua requisição. Tente novamente mais tarde" });
            }
        }
    }
}