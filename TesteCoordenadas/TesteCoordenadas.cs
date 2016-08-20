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
        public void testeCalculoDistanciaValido()
        {
            Util util = new Util();
            var distancia = util.CalcularDistancia(2, 23, 22, 15);
            Assert.IsTrue(distancia > 0);
        }
    
        [TestMethod]
        public void testeCalculoDistanciaInvalido()
        {
            Util util = new Util();
            double distancia = 0;

            try
            {
                distancia = util.CalcularDistancia(0, 0, 0, 0);
            }
            catch (Exception)
            {
                distancia = 0;
            }
            Assert.IsFalse(distancia > 0);
        }

        [TestMethod]
        public void testeCriarBase()
        {
            Util util = new Util();
            DataTable table = util.CriarBase();
            Assert.IsTrue(table.Rows.Count > 0);
        }
    }
}
