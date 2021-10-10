﻿using laboratorio_dev_prosperi.Models;
using laboratorio_dev_prosperi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi.Data
{
    interface IContextDb
    {
        ResponseMessage AdicionarNovaOrdemDeServico(OrdemDeServico ordemDeServico);
        ResponseMessage BuscarTodasAsOrdensDeServico();
        ResponseMessage BuscarOrdemDeServicoPorNumero(int numeroServico);
        ResponseMessage AlterarOrdemDeServico(OrdemDeServico ordemDeServico);
        ResponseMessage RemoverOrdemDeServico(int numeroServico);
    }
}
