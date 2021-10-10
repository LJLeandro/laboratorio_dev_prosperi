using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi.Models
{
    public class OrdemDeServico
    {
        [JsonPropertyName("numero_servico")]
        public int NumeroServico { get; set; }
        [JsonPropertyName("titulo_servico")]
        public string TituloServico { get; set; }
        [JsonPropertyName("cnpj")]
        public string CNPJ { get; set; }
        [JsonPropertyName("valor")]
        public double Valor { get; set; }
        [JsonPropertyName("nome_cliente")]
        public string NomeCliente { get; set; }
        [JsonPropertyName("cpf_prestador")]
        public string CPFPrestador { get; set; }
        [JsonPropertyName("nome_prestador")]
        public string NomePrestador { get; set; }
        [JsonPropertyName("data_execucao_servico")]
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
