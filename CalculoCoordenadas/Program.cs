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
                    Console.WriteLine("Favor entrar com um nome válido.");
                else
                {
                    qtdPessoas = tbPessoa.Select("Nome = '" + line + "'").Count();
                    if (qtdPessoas == 0)
                    {
                        Console.WriteLine("Nome não encontrado! Favor entrar com um nome válido.");
                    }
                }
            } while (line == string.Empty || qtdPessoas == 0);


            //Cálcular a distância entre pontos
            DataRow drPessoaSelecionada = tbPessoa.Select("Nome = '" + line + "'").FirstOrDefault();
            List<DataRow> listPessoa = tbPessoa.AsEnumerable().ToList();
            foreach (DataRow itemPessoa in listPessoa)
            {
                itemPessoa["Distancia"] = 0;
                if (itemPessoa["Nome"].ToString() != line)
                    itemPessoa["Distancia"] = new Util().CalcularDistancia(Convert.ToDouble(drPessoaSelecionada["Latitude"]), Convert.ToDouble(drPessoaSelecionada["Longitude"]), Convert.ToDouble(itemPessoa["Latitude"]), Convert.ToDouble(itemPessoa["Longitude"]));
            }


            //Selecionar as 3 pessoas mais próximas
            tbPessoa.DefaultView.Sort = "Distancia asc";
            tbPessoa = tbPessoa.DefaultView.ToTable(true);
            var drProximas = tbPessoa.Select("Distancia <> 0").Take(3);
            Console.WriteLine("Resultado da consulta:");
            foreach (DataRow itemProxima in drProximas)
                Console.WriteLine("Pessoa: " + itemProxima["Nome"].ToString() + " Distância: " + Decimal.Parse(itemProxima["Distancia"].ToString()).ToString("0.##") + "Km");
            
            Console.WriteLine("Fim da consulta.");
            Console.ReadLine();
        }
    }
}
