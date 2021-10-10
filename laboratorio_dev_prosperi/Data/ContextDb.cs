using laboratorio_dev_prosperi.Models;
using laboratorio_dev_prosperi.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace laboratorio_dev_prosperi.Data
{
    public class ContextDb : IContextDb
    {
        private const string _arquivoDados = "dados.txt";

        public ContextDb()
        {
            if (!File.Exists(_arquivoDados))
                File.Create(_arquivoDados);
        }

        public ResponseMessage AdicionarNovaOrdemDeServico(OrdemDeServico ordemDeServicoNova)
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
                        ordemDeServicoNova.NumeroServico = (responseTodasAsOrdensDeServico.Retorno as List<OrdemDeServico>).Last().NumeroServico + 1;
                    } 
                    else
                    {
                        ordemDeServicoNova.NumeroServico = 1;
                    }
                }

                using StreamWriter writer = File.AppendText(_arquivoDados);
                writer.WriteLine(ordemDeServicoNova.ToString());

                response.Mensagem = "Ordem de Serviço grava com sucesso.";
                response.Sucesso = true;
                response.Retorno = ordemDeServicoNova;
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
                OrdemDeServico ordemParaRemocao = BuscarOrdemDeServicoPorNumero(ordemDeServicoAlterada.NumeroServico).Retorno as OrdemDeServico;

                List<OrdemDeServico> ordens = BuscarTodasAsOrdensDeServico().Retorno as List<OrdemDeServico>;

                int posicaoOrdem = ordens.IndexOf(ordemDeServicoAlterada); 

                ordens[posicaoOrdem] = ordemDeServicoAlterada;
                LimparArquivo();

                foreach (OrdemDeServico ordem in ordens)
                    AdicionarNovaOrdemDeServico(ordem);

                response.Mensagem = $"Ordem de Serviço nº {ordemDeServicoAlterada.NumeroServico} alterado.";
                response.Sucesso = true;
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
                using StreamReader reader = new(_arquivoDados);
                string[] linhas = reader.ReadToEnd().Split('\u002C');

                if (linhas.Length > 0)
                {
                    foreach (string dados in linhas)
                    {
                        if (int.Parse(dados.Split(";")[0]) == numeroServico)
                        {
                            OrdemDeServico ordem = new()
                            {
                                NumeroServico = numeroServico,
                                TituloServico = dados[1].ToString(),
                                CNPJ = dados[2].ToString(),
                                Valor = double.Parse(dados[3].ToString()),
                                NomeCliente = dados[4].ToString(),
                                CPFPrestador = dados[5].ToString(),
                                NomePrestador = dados[6].ToString(),
                                DataExecucaoServico = DateTime.Parse(dados[7].ToString())
                            };

                            response.Mensagem = "Dados Recuperados";
                            response.Retorno = ordem;
                            response.Sucesso = true;

                            break;
                        }
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
                string[] linhas = reader.ReadToEnd().Split('\u002C');

                foreach (var dados in linhas)
                {
                    OrdemDeServico ordemDeServico = new()
                    {
                        NumeroServico = int.Parse(dados[0].ToString()),
                        TituloServico = dados[1].ToString(),
                        CNPJ = dados[2].ToString(),
                        Valor = double.Parse(dados[3].ToString()),
                        NomeCliente = dados[4].ToString(),
                        CPFPrestador = dados[5].ToString(),
                        NomePrestador = dados[6].ToString(),
                        DataExecucaoServico = DateTime.Parse(dados[7].ToString())
                    };

                    (response.Retorno as List<OrdemDeServico>).Add(ordemDeServico);
                }

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
                OrdemDeServico ordemParaRemocao = BuscarOrdemDeServicoPorNumero(numeroServico).Retorno as OrdemDeServico;

                List<OrdemDeServico> ordens = BuscarTodasAsOrdensDeServico().Retorno as List<OrdemDeServico>;

                ordens.Remove(ordemParaRemocao);

                LimparArquivo();

                foreach (OrdemDeServico ordem in ordens)
                    AdicionarNovaOrdemDeServico(ordem);

                response.Mensagem = $"Ordem de Serviço nº {numeroServico} removidos";
                response.Sucesso = true;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Retorno = null;
            }

            return response;
        }
    
        private void LimparArquivo()
        {
            File.Delete(_arquivoDados);
        }
    }
}
