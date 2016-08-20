using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCoordenadas.Negocio
{
    public class Util
    {
        /// <summary>
        /// CriarBase
        /// </summary>
        /// <returns></returns>
        public DataTable CriarBase()
        {
            DataTable tbPessoa = new DataTable();
            tbPessoa.TableName = "tbPessoa";
            tbPessoa.Columns.Add(new DataColumn("Nome", typeof(string)));
            tbPessoa.Columns.Add(new DataColumn("Latitude", typeof(decimal)));
            tbPessoa.Columns.Add(new DataColumn("Longitude", typeof(decimal)));
            tbPessoa.Columns.Add(new DataColumn("Distancia", typeof(decimal)));

            DataColumn[] keys = new DataColumn[1];
            keys[0] = tbPessoa.Columns[0];
            tbPessoa.PrimaryKey = keys;

            tbPessoa.Rows.Add("Marcos", 25, 16);
            tbPessoa.Rows.Add("João", 28, 86);
            tbPessoa.Rows.Add("Maria", 29, 96);
            tbPessoa.Rows.Add("Antonio", 30, 44);
            tbPessoa.Rows.Add("Cassio", 54, 46);
            tbPessoa.Rows.Add("Lucas", 77, 30);
            tbPessoa.Rows.Add("Carla", 15, 21);

            return tbPessoa;
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
        public DataTable CalcularDistancia(DataTable tbPessoa, string nomePessoa) 
        {
            //Cálcular a distância entre pontos
            DataRow drPessoaSelecionada = tbPessoa.Select("Nome = '" + nomePessoa + "'").FirstOrDefault();

            if (drPessoaSelecionada == null) return tbPessoa;

            List<DataRow> listPessoa = tbPessoa.AsEnumerable().ToList();
            foreach (DataRow itemPessoa in listPessoa)
            {
                itemPessoa["Distancia"] = 0;
                if (itemPessoa["Nome"].ToString().ToUpper().Trim() != nomePessoa.ToUpper().Trim())
                    itemPessoa["Distancia"] = new Util().CalcularDistancia(Convert.ToDouble(drPessoaSelecionada["Latitude"]), Convert.ToDouble(drPessoaSelecionada["Longitude"]), Convert.ToDouble(itemPessoa["Latitude"]), Convert.ToDouble(itemPessoa["Longitude"]));
            }
            return tbPessoa;
        }

        /// <summary>
        /// Selecionar as 3 pessoas mais próximas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataRow> SelecionarProximas(DataTable tbPessoa)
        {
            tbPessoa.DefaultView.Sort = "Distancia asc";
            tbPessoa = tbPessoa.DefaultView.ToTable(true);
            return tbPessoa.Select("Distancia <> 0").Take(3);
        }
    }
}
