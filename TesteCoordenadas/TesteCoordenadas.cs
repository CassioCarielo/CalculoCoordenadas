using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculoCoordenadas.Negocio;
using System.Data;

namespace TesteCoordenadas
{
    [TestClass]
    public class TesteCoordenadas
    {
        [TestMethod]
        public void testeCriarBase()
        {
            DataTable table = new Util().CriarBase();
            Assert.IsTrue(table.Rows.Count > 0);
        }

        [TestMethod]
        public void testeCalculoPontosValido()
        {
            var distancia = new Util().CalcularDistancia(2, 23, 22, 15);
            Assert.IsTrue(distancia > 0);
        }
    
        [TestMethod]
        public void testeCalculoPontosInvalido()
        {
            double distancia = 0;

            try
            {
                distancia = new Util().CalcularDistancia(0, 0, 0, 0);
            }
            catch (Exception)
            {
                distancia = 0;
            }
            Assert.IsFalse(distancia > 0);
        }

        [TestMethod]
        public void testeCalculoDistanciaValido()
        {
            DataTable tbPessoa = new Util().CriarBase();

            tbPessoa = new Util().CalcularDistancia(tbPessoa, "Maria");
            Assert.IsTrue(tbPessoa.Rows.Count > 0);
        }

        [TestMethod]
        public void testeCalculoDistanciaInvalido()
        {
            DataTable tbPessoa = new Util().CriarBase();
            bool achouPessoa = false;

            tbPessoa = new Util().CalcularDistancia(tbPessoa, "Teste");
            foreach (DataRow item in tbPessoa.Rows)
            {
                achouPessoa = item["Distancia"].Equals("0");
                if (achouPessoa) break;
            }
            Assert.IsFalse(achouPessoa);
        }
    }
}
