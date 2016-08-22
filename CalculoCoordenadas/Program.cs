using CalculoCoordenadas.Dados;
using CalculoCoordenadas.Entidades;
using CalculoCoordenadas.Negocio;
using System;
using System.Collections.Generic;

namespace CalculoCoordenadas
{
    class Program
    {
        static void Main(string[] args)
        {
            string nomeInformado = string.Empty;
            

            Console.WriteLine("=-Consultar as três pessoas mais próximas de uma informada-=");
            Console.WriteLine("");


            //Consultar cadastro de Pessoas
            List<Pessoa> listPessoa = new DtPessoa().Consultar();

            
            //Exibe as pessoas cadastradas
            Console.WriteLine("Lista de Pessoas cadastradas.");
            foreach (Pessoa pessoa in listPessoa)
                Console.WriteLine("Nome: " + pessoa.Nome + " Latitude: " + pessoa.Latitude + " Longitude: " + pessoa.Longitude);


            Console.WriteLine("");
            Console.WriteLine("Entre com o nome da Pessoa:");


            //Procurar nome na lista
            Pessoa pessoaInformada = new Pessoa();
            do
            {
                nomeInformado = Console.ReadLine();
                if (nomeInformado == string.Empty)
                    Console.WriteLine("Entre com um nome válido.");
                else
                {
                    pessoaInformada = new BsCoordenadas().ProcuraPessoa(listPessoa, nomeInformado);
                    if (pessoaInformada == null)
                        Console.WriteLine("Nome não encontrado! Entre com um nome da lista.");
                }
            } while (nomeInformado == string.Empty || pessoaInformada == null);


            //Cálcular a distância entre pontos
            listPessoa = new BsCoordenadas().CalcularDistancia(listPessoa, pessoaInformada);


            //Selecionar as 3 pessoas mais próximas
            List<Pessoa> listPessoaSelecionadas = new BsCoordenadas().SelecionarProximas(listPessoa, pessoaInformada);
            Console.WriteLine("Resultado da consulta:");
            foreach (Pessoa pessoa in listPessoaSelecionadas)
                Console.WriteLine("Pessoa: " + pessoa.Nome + " Distância: " + pessoa.Distancia.ToString("0.##") + "Km");
            
            Console.WriteLine("Fim da consulta.");
            Console.ReadLine();
        }
    }
}
