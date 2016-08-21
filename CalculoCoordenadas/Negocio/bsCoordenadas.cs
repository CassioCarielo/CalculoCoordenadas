using CalculoCoordenadas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculoCoordenadas.Negocio
{
    public class bsCoordenadas
    {
        /// <summary>
        /// CriarBase
        /// </summary>
        /// <returns></returns>
        public List<Pessoa> CriarLista()
        {
            List<Pessoa> listPessoa = new List<Pessoa>();
            listPessoa.Add(new Pessoa { Nome = "Marcos", Latitude = 25, Longitude = 16, Distancia = 0 });
            listPessoa.Add(new Pessoa { Nome = "João", Latitude = 28, Longitude = 86, Distancia = 0 });
            listPessoa.Add(new Pessoa { Nome = "Maria", Latitude = 29, Longitude = 96, Distancia = 0 });
            listPessoa.Add(new Pessoa { Nome = "Antonio", Latitude = 30, Longitude = 44, Distancia = 0 });
            listPessoa.Add(new Pessoa { Nome = "Cassio", Latitude = 54, Longitude = 46, Distancia = 0 });
            listPessoa.Add(new Pessoa { Nome = "Lucas", Latitude = 77, Longitude = 30, Distancia = 0 });
            listPessoa.Add(new Pessoa { Nome = "Carla", Latitude = 15, Longitude = 21, Distancia = 0 });

            return listPessoa;
        }

        /// <summary>
        /// CalcularDistancia - Calcula a Distância entre dois pontos
        /// </summary>
        /// <param name="origem_lat"></param>
        /// <param name="origem_lng"></param>
        /// <param name="destino_lat"></param>
        /// <param name="destino_lng"></param>
        /// <returns>Retorna a distância entre dois pontos</returns>
        public double CalcularDistancia(double origem_lat, double origem_lng, double destino_lat, double destino_lng)
        {
            double x1 = origem_lat;
            double x2 = destino_lat;
            double y1 = origem_lng;
            double y2 = destino_lng;

            // ARCO AB = c 
            double c = 90 - (y2);

            // ARCO AC = b 
            double b = 90 - (y1);

            // Arco ABC = a 
            // Diferença das longitudes: 
            double a = x2 - x1;

            // Formula: cos(a) = cos(b) * cos(c) + sen(b)* sen(c) * cos(A) 
            double cos_a = Math.Cos(b) * Math.Cos(c) + Math.Sin(c) * Math.Sin(b) * Math.Cos(a);

            double arc_cos = Math.Acos(cos_a);

            // 2 * pi * Raio da Terra = 6,28 * 6.371 = 40.030 Km 
            // 360 graus = 40.030 Km 
            // 3,2169287 = x 
            // x = (40.030 * 3,2169287)/360 = 357,68 Km 
            double distancia = (40030 * arc_cos) / 360;

            return distancia;
        }

        /// <summary>
        /// Calcular a Distância de todas as Pessoas
        /// </summary>
        /// <param name="tbPessoa"></param>
        /// <param name="nomePessoa"></param>
        /// <returns></returns>
        public List<Pessoa> CalcularDistancia(List<Pessoa> listPessoa, string nomeInformado)
        {
            //Cálcular a distância entre pontos
            Pessoa pessoaEncontrada = listPessoa.Find(x => x.Nome == nomeInformado);
            if (pessoaEncontrada == null) return new List<Pessoa>();

            foreach (var pessoa in listPessoa)
                pessoa.Distancia = new bsCoordenadas().CalcularDistancia(Convert.ToDouble(pessoaEncontrada.Latitude), Convert.ToDouble(pessoaEncontrada.Longitude), Convert.ToDouble(pessoa.Latitude), Convert.ToDouble(pessoa.Longitude));

            return listPessoa;
        }

        /// <summary>
        /// Procura a pessoa informada numa lista cadastrada
        /// </summary>
        /// <param name="listPessoa"></param>
        /// <param name="nomeInformado"></param>
        /// <returns></returns>
        public Pessoa ProcuraPessoa(List<Pessoa> listPessoa, string nomeInformado)
        {
            return listPessoa.Find(x => x.Nome == nomeInformado);
        }

        /// <summary>
        /// Selecionar as 3 pessoas mais próximas
        /// </summary>
        /// <returns></returns>
        public List<Pessoa> SelecionarProximas(List<Pessoa> listPessoa, string nomeInformado)
        {
            listPessoa.Remove(ProcuraPessoa(listPessoa, nomeInformado));
            return listPessoa.OrderBy(x => x.Distancia).Take(3).ToList();
        }
    }
}
