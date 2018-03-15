using AplicacaoDemo.App.Models;
using AplicacaoDemo.Dominio.Entidade;
using AplicacaoDemo.Dominio.Servico;
using AplicacaoDemo.Dominio.Utils;
using AplicacaoDemo.Repositorio.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplicacaoDemo.App.Controllers
{
    public class VinhoController : Controller
    {
        VinhoServico _vinhoServico = new VinhoServico(new VinhoRepositorio());

        public async Task<JsonResult> RetornaTodos(int pagina, int tamanhoPagina)
        {
            ListaPaginada<Vinho> lista = await _vinhoServico.RetornaTodos(pagina, tamanhoPagina);

            var listaModel = new List<VinhoViewModel>();
            foreach (Vinho i in lista)
            {
                listaModel.Add(new VinhoViewModel()
                {
                    Id = i.Id,
                    Nome = i.Descricao,
                    Tipo = i.TipoVinho.ToString(),
                    Peso = i.Peso,
                    Valor = i.Valor
                });
            }

            return Json(new { lista = listaModel, count = lista.TotalCount, pageCount = lista.PageCount });
        }

        public async Task<JsonResult> Salvar(VinhoViewModel model)
        {
            try
            {
                var vinho = new Vinho
                {
                    Id = model.Id,
                    Peso = model.Peso,
                    Descricao = model.Nome,
                    Valor = model.Valor,
                    TipoVinho = Dominio.Enumerador.TipoVinho.Seco,
                };

                await _vinhoServico.Salvar(vinho);

                return Json(new { isValid = true, message = "Este vinho foi cadastrado com sucesso" });

            }
            catch
            {
                return Json(new { isValid = false, message = "Ocorreu um erro com a sua requisição. Tente novamente mais tarde" });
            }
        }
    }
}