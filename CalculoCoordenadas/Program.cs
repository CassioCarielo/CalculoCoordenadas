using CalculoCoordenadas.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCoordenadas
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = string.Empty;

            
            Console.WriteLine("=-Consultar as três pessoas mais próximas de uma informada-=");
            Console.WriteLine("");


            //Exibe as pessoas cadastradas
            DataTable tbPessoa = new Util().CriarBase();
            Console.WriteLine("Lista de Pessoas cadastradas.");
            foreach (DataRow itemPessoa in tbPessoa.Rows)
                Console.WriteLine("Nome: " + itemPessoa["Nome"] + " Latitude: " + itemPessoa["Latitude"] + " Longitude: " + itemPessoa["Longitude"]);


            Console.WriteLine("");
            Console.WriteLine("Entre com o nome da Pessoa:");


            //Procurar nome na lista
            int qtdPessoas = 0;
            do
            {
                line = Console.ReadLine();
                if (line == string.Empty)
                    Console.WriteLine("Entre com um nome válido.");
                else
                {
                    qtdPessoas = tbPessoa.Select("Nome = '" + line + "'").Count();
                    if (qtdPessoas == 0)
                        Console.WriteLine("Nome não encontrado! Entre com um nome da lista.");
                }
            } while (line == string.Empty || qtdPessoas == 0);


            //Cálcular a distância entre pontos
            tbPessoa = new Util().CalcularDistancia(tbPessoa, line);


            //Selecionar as 3 pessoas mais próximas
            IEnumerable<DataRow> drProximas = new Util().SelecionarProximas(tbPessoa);
            Console.WriteLine("Resultado da consulta:");
            foreach (DataRow itemProxima in drProximas)
                Console.WriteLine("Pessoa: " + itemProxima["Nome"].ToString() + " Distância: " + Decimal.Parse(itemProxima["Distancia"].ToString()).ToString("0.##") + "Km");
            
            Console.WriteLine("Fim da consulta.");
            Console.ReadLine();
        }
    }
}
