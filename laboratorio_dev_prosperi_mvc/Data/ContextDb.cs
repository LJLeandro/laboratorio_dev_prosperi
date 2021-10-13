using laboratorio_dev_prosperi.Utils;
using laboratorio_dev_prosperi_mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi.Data
{
    public class ContextDb : IContextDb
    {
        private const string _arquivoDados = "dados.json";

        public ContextDb()
        {
            if (!File.Exists(_arquivoDados))
            {
                using StreamWriter writer = new StreamWriter(File.Create(_arquivoDados));
                writer.Write("[]");
                writer.Close();
            }
        }

        public ResponseMessage AdicionarNovaOrdemDeServico(OrdemDeServico novaOrdemDeServico)
        {
            ResponseMessage response = new()
            {
                Sucesso = false
            };

            try
            {
                ResponseMessage responseTodasAsOrdensDeServico = BuscarTodasAsOrdensDeServico();

                if (responseTodasAsOrdensDeServico.Sucesso == true)
                {
                    if ((responseTodasAsOrdensDeServico.Retorno as List<OrdemDeServico>).Count > 0)
                    {
                        novaOrdemDeServico.NumeroServico = (responseTodasAsOrdensDeServico.Retorno as List<OrdemDeServico>).Last().NumeroServico + 1;
                    } 
                    else
                    {
                        novaOrdemDeServico.NumeroServico = 1;
                    }

                    (responseTodasAsOrdensDeServico.Retorno as List<OrdemDeServico>).Add(novaOrdemDeServico);

                    File.WriteAllText(_arquivoDados, JsonSerializer.Serialize((responseTodasAsOrdensDeServico.Retorno as List<OrdemDeServico>)));

                    response.Mensagem = "Ordem de Serviço grava com sucesso.";
                    response.Sucesso = true;
                    response.Retorno = novaOrdemDeServico;
                }  
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Retorno = null;
            }

            return response;
        }

        public ResponseMessage AlterarOrdemDeServico(OrdemDeServico ordemDeServicoAlterada)
        {
            ResponseMessage response = new()
            {
                Sucesso = false,
            };

            try
            {
                List<OrdemDeServico> ordens = BuscarTodasAsOrdensDeServico().Retorno as List<OrdemDeServico>;

                if(ordens.Exists(x => x.NumeroServico == ordemDeServicoAlterada.NumeroServico))
                {
                    int posicaoOrdem = ordens.FindIndex(x => x.NumeroServico == ordemDeServicoAlterada.NumeroServico); 
                    ordens[posicaoOrdem] = ordemDeServicoAlterada;

                    File.WriteAllText(_arquivoDados, JsonSerializer.Serialize(ordens));

                    response.Mensagem = $"Ordem de Serviço nº {ordemDeServicoAlterada.NumeroServico} alterado.";
                    response.Sucesso = true;
                }
                else
                {
                    response.Mensagem = $"Ordem de Serviço nº {ordemDeServicoAlterada.NumeroServico} não existe.";
                    response.Sucesso = true;
                }                
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Retorno = null;
            }

            return response;
        }

        public ResponseMessage BuscarOrdemDeServicoPorNumero(int numeroServico)
        {
            ResponseMessage response = new()
            {
                Sucesso = false
            };

            try
            {
                ResponseMessage responseBuscarTodasAsOrdensDeServico = BuscarTodasAsOrdensDeServico();

                if (responseBuscarTodasAsOrdensDeServico.Sucesso == true)
                {
                    OrdemDeServico ordemDeServicoProcurada = (responseBuscarTodasAsOrdensDeServico.Retorno as List<OrdemDeServico>)
                                                                .FirstOrDefault(x => x.NumeroServico == numeroServico);

                    if (ordemDeServicoProcurada !=  null)
                    {
                        response.Mensagem = "Dados Recuperados";
                        response.Retorno = ordemDeServicoProcurada;
                        response.Sucesso = true;
                    }
                    else
                    {
                        response.Mensagem = "Ordem de Serviço não encontrada.";
                        response.Retorno = null;
                        response.Sucesso = true;
                    }
                } 
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Retorno = null;
            }

            return response;
        }

        public ResponseMessage BuscarTodasAsOrdensDeServico()
        {
            ResponseMessage response = new()
            {
                Sucesso = false,
                Retorno = new List<OrdemDeServico>()
            };

            try
            {
                using StreamReader reader = new(_arquivoDados);
                response.Retorno = JsonSerializer.Deserialize<List<OrdemDeServico>>(reader.ReadToEnd());
                reader.Close();

                response.Mensagem = "Dados Recuperados";
                response.Sucesso = true;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Retorno = null;
            }

            return response;
        }

        public ResponseMessage RemoverOrdemDeServico(int numeroServico)
        {
            ResponseMessage response = new()
            {
                Sucesso = false,
            };

            try
            {
                List<OrdemDeServico> ordens = BuscarTodasAsOrdensDeServico().Retorno as List<OrdemDeServico>;
                int posicaoOrdem = ordens.FindIndex(x => x.NumeroServico == numeroServico);

                if (posicaoOrdem > -1)
                {
                    ordens.RemoveAt(posicaoOrdem);
                    File.WriteAllText(_arquivoDados, JsonSerializer.Serialize(ordens));

                    response.Mensagem = $"Ordem de Serviço nº {numeroServico} removida.";
                    response.Sucesso = true;
                }
                else
                {
                    response.Mensagem = $"Ordem de Serviço nº {numeroServico} não existe.";
                    response.Sucesso = true;
                }
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Retorno = null;
            }

            return response;
        }
    
    }
}
