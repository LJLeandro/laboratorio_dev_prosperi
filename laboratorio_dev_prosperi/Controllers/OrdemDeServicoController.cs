using laboratorio_dev_prosperi.Data;
using laboratorio_dev_prosperi.Models;
using laboratorio_dev_prosperi.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace laboratorio_dev_prosperi.Controllers
{
    [Route("api/ordemdeservico")]
    [ApiController]
    public class OrdemDeServicoController : ControllerBase
    {
        private IContextDb context = new ContextDb();

        // GET: api/ordemdeservico
        [HttpGet]
        public ResponseMessage Get()
        {
            ResponseMessage response = context.BuscarTodasAsOrdensDeServico();

            return response;
        }

        // GET api/ordemdeservico/5
        [HttpGet("{numeroServico}")]
        public ResponseMessage Get(int numeroServicoProcurado)
        {
            ResponseMessage response = context.BuscarOrdemDeServicoPorNumero(numeroServicoProcurado);

            return response;
        }

        // POST api/ordemdeservico
        [HttpPost]
        public ResponseMessage Post(OrdemDeServico novaOrdemDeServico)
        {
            ResponseMessage response = context.AdicionarNovaOrdemDeServico(novaOrdemDeServico);

            return response;
        }

        // PUT api/ordemdeservico/5
        [HttpPut]
        public ResponseMessage Put(OrdemDeServico ordemDeServicoAlterada)
        {
            ResponseMessage response = context.AlterarOrdemDeServico(ordemDeServicoAlterada);

            return response;
        }

        // DELETE api/ordemdeservico/5
        [HttpDelete("{numeroServico}")]
        public ResponseMessage Delete(int numeroServicoParaRemocao)
        {
            ResponseMessage response = context.RemoverOrdemDeServico(numeroServicoParaRemocao);

            return response;
        }
    }
}
