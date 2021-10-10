using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi.Models
{
    public class OrdemDeServico
    {
        public int NumeroServico { get; set; }
        public string TituloServico { get; set; }
        public string CNPJ { get; set; }
        public double Valor { get; set; }
        public string NomeCliente { get; set; }
        public string CPFPrestador { get; set; }
        public string NomePrestador { get; set; }
        public DateTime DataExecucaoServico { get; set; }

        public override string ToString()
        {
            return $"{NumeroServico};" +
                $"{TituloServico};" +
                $"{CNPJ};" +
                $"{Valor};" +
                $"{NomeCliente};" +
                $"{CPFPrestador};" +
                $"{NomePrestador};" +
                $"{DataExecucaoServico:dd-MM-yyyy hh:mm:ss}";
        }
    }
}
