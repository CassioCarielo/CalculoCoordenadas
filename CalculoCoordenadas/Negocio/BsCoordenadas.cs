﻿using CalculoCoordenadas.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CalculoCoordenadas.Negocio
{
    public class BsCoordenadas
    {
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
        public List<Pessoa> CalcularDistancia(List<Pessoa> listPessoa, Pessoa pessoaInformada)
        {
            //Cálcular a distância entre pontos
            if (pessoaInformada == null) return new List<Pessoa>();

            foreach (var pessoa in listPessoa)
                pessoa.Distancia = new BsCoordenadas().CalcularDistancia(Convert.ToDouble(pessoaInformada.Latitude), Convert.ToDouble(pessoaInformada.Longitude), Convert.ToDouble(pessoa.Latitude), Convert.ToDouble(pessoa.Longitude));

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
        public List<Pessoa> SelecionarProximas(List<Pessoa> listPessoa, Pessoa pessoaInformada)
        {
            int qtdPessoas = Convert.ToInt32(ConfigurationManager.AppSettings["QtdPessoas"].ToString());

            listPessoa.Remove(ProcuraPessoa(listPessoa, pessoaInformada.Nome));
            return listPessoa.OrderBy(x => x.Distancia).Take(qtdPessoas).ToList();
        }
    }
}
