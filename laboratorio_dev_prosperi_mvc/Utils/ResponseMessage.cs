using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi.Utils
{
    public class ResponseMessage
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Retorno { get; set; }
    }
}
