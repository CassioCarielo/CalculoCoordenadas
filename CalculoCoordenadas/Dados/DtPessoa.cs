using CalculoCoordenadas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCoordenadas.Dados
{
    public class DtPessoa
    {
        /// <summary>
        /// CriarBase
        /// </summary>
        /// <returns></returns>
        public List<Pessoa> Consultar()
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
    }
}
