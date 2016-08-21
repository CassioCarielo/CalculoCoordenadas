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


            //Exibe as pessoas cadastradas
            List<Pessoa> listPessoa = new bsCoordenadas().CriarLista();
            Console.WriteLine("Lista de Pessoas cadastradas.");
            foreach (Pessoa pessoa in listPessoa)
                Console.WriteLine("Nome: " + pessoa.Nome + " Latitude: " + pessoa.Latitude + " Longitude: " + pessoa.Longitude);


            Console.WriteLine("");
            Console.WriteLine("Entre com o nome da Pessoa:");


            //Procurar nome na lista
            Pessoa pessoaEncontrada = new Pessoa();
            do
            {
                nomeInformado = Console.ReadLine();
                if (nomeInformado == string.Empty)
                    Console.WriteLine("Entre com um nome válido.");
                else
                {
                    pessoaEncontrada = new bsCoordenadas().ProcuraPessoa(listPessoa, nomeInformado);
                    if (pessoaEncontrada == null)
                        Console.WriteLine("Nome não encontrado! Entre com um nome da lista.");
                }
            } while (nomeInformado == string.Empty || pessoaEncontrada == null);


            //Cálcular a distância entre pontos
            listPessoa = new bsCoordenadas().CalcularDistancia(listPessoa, nomeInformado);


            //Selecionar as 3 pessoas mais próximas
            List<Pessoa> listPessoaSelecionadas = new bsCoordenadas().SelecionarProximas(listPessoa, nomeInformado);
            Console.WriteLine("Resultado da consulta:");
            foreach (Pessoa pessoa in listPessoaSelecionadas)
                Console.WriteLine("Pessoa: " + pessoa.Nome + " Distância: " + pessoa.Distancia.ToString("0.##") + "Km");
            
            Console.WriteLine("Fim da consulta.");
            Console.ReadLine();
        }
    }
}
