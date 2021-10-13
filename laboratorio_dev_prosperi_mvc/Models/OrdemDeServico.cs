using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi_mvc.Models
{
    public class OrdemDeServico
    {
        [Display(Name = "Numero de Serviço")]
        public int NumeroServico { get; set; }

        [Display(Name = "Título de Serviço")]
        [Required(ErrorMessage = "Insira um Título de Serviço")]
        public string TituloServico { get; set; }

        [Required(ErrorMessage = "Insira o CNPJ do Cliente")]
        public string CNPJ { get; set; }

        [Display(Name = "Valor do Serviço")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "Insira o Valor do Serviço")]
        public double Valor { get; set; }

        [Display(Name = "Nome do Cliente")]
        [Required(ErrorMessage = "Insira o Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Display(Name = "CPF do Prestador de Serviço")]
        [Required(ErrorMessage = "Insira o CPF do Prestador de Serviço")]
        public string CPFPrestador { get; set; }

        [Display(Name = "Nome do Prestador de Serviç")]
        [Required(ErrorMessage = "Insira o Nome do Prestador de Serviço")]
        public string NomePrestador { get; set; }

        [Display(Name = "Data de Execução do Serviço")]
        [Required(ErrorMessage = "Marque a Data de Execução do Serviço")]
        public DateTime DataExecucaoServico { get; set; }
    }
}
