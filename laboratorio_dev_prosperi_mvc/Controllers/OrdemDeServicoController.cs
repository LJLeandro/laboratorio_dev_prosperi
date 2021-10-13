using laboratorio_dev_prosperi.Data;
using laboratorio_dev_prosperi.Utils;
using laboratorio_dev_prosperi_mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi_mvc.Controllers
{
    public class OrdemDeServicoController : Controller
    {
        private IContextDb context = new ContextDb();

        // GET: OrdemDeServicoController
        public ActionResult Index()
        {
            List<OrdemDeServico> ordens = new();
            ResponseMessage response = context.BuscarTodasAsOrdensDeServico();

            if (response.Sucesso == true)
            {
                ordens = response.Retorno as List<OrdemDeServico>;
            }

            return View(ordens);
        }

        // GET: OrdemDeServicoController/Details/5
        public ActionResult Details(int numeroDeServico)
        {
            ResponseMessage response = context.BuscarOrdemDeServicoPorNumero(numeroDeServico);
            OrdemDeServico ordemDeServicoParaAlteracao = new();

            if (response.Sucesso == true)
            {
                ordemDeServicoParaAlteracao = response.Retorno as OrdemDeServico;
                return View(ordemDeServicoParaAlteracao);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: OrdemDeServicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdemDeServicoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdemDeServico novaOrdemDeServico)
        {
            try
            {
                ResponseMessage response = context.AdicionarNovaOrdemDeServico(novaOrdemDeServico);

                if (response.Sucesso == true)
                {
                    CarregarRespostaAcao(response);
                    return RedirectToAction(nameof(Index));
                }                
                else
                {
                    throw new Exception(response.Mensagem);
                }
            }
            catch(Exception ex)
            {
                CarregarRespostaAcao(new ResponseMessage() { Mensagem = ex.Message, Sucesso = false});
            }

            return View(novaOrdemDeServico);
        }

        // GET: OrdemDeServicoController/Edit/5
        public ActionResult Edit(int numeroDeServico)
        {
            ResponseMessage response = context.BuscarOrdemDeServicoPorNumero(numeroDeServico);
            OrdemDeServico ordemDeServicoParaAlteracao = new();

            if (response.Sucesso == true)
            {
                ordemDeServicoParaAlteracao = response.Retorno as OrdemDeServico;
                return View(ordemDeServicoParaAlteracao);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }            
        }

        // POST: OrdemDeServicoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrdemDeServico ordemDeServicoAlterada) 
        {
            try
            {
                ResponseMessage response = context.AlterarOrdemDeServico(ordemDeServicoAlterada);

                if (response.Sucesso == true)
                {
                    CarregarRespostaAcao(response);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception(response.Mensagem);
                }
            }
            catch (Exception ex)
            {
                CarregarRespostaAcao(new ResponseMessage() { Mensagem = ex.Message, Sucesso = false });
            }

            return View(ordemDeServicoAlterada);
        }

        // GET: OrdemDeServicoController/Delete/5
        public ActionResult Delete(int numeroDeServico)
        {
            ResponseMessage response = context.RemoverOrdemDeServico(numeroDeServico);

            return RedirectToAction(nameof(Index));
        }

        private void CarregarRespostaAcao(ResponseMessage message)
        {
            ViewBag.FormResponse = message;
        }
    }
}
