using CalculoCoordenadas.Entidades;
using CalculoCoordenadas.Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TesteCoordenadas
{
    [TestClass]
    public class TesteCoordenadas
    {
        [TestMethod]
        public void TesteCriarLista()
        {
            List<Pessoa> listPessoa = new bsCoordenadas().CriarLista();

            Assert.IsTrue(listPessoa.Count > 0);
        }

        [TestMethod]
        public void TesteCalculoPontosValido()
        {
            var distancia = new bsCoordenadas().CalcularDistancia(2, 23, 22, 15);

            Assert.IsTrue(distancia > 0);
        }
    
        [TestMethod]
        public void TesteCalculoPontosInvalido()
        {
            double distancia = 0;

            distancia = new bsCoordenadas().CalcularDistancia(0, 0, 0, 0);
            Assert.IsFalse(distancia > 0);
        }

        [TestMethod]
        public void TesteCalculoDistanciaValido()
        {
            List<Pessoa> listPessoa = new bsCoordenadas().CriarLista();
            
            listPessoa = new bsCoordenadas().CalcularDistancia(listPessoa, "Maria");
            Assert.IsTrue(listPessoa.Count > 0);
        }

        [TestMethod]
        public void TesteCalculoDistanciaInvalido()
        {
            List<Pessoa> listPessoa = new bsCoordenadas().CriarLista();

            listPessoa = new bsCoordenadas().CalcularDistancia(listPessoa, "Teste");
            Assert.IsFalse(listPessoa.Count > 0);
        }

        [TestMethod]
        public void TesteProcurarPessoaValido()
        {
            List<Pessoa> listPessoa = new bsCoordenadas().CriarLista();

            Pessoa pessoa = new bsCoordenadas().ProcuraPessoa(listPessoa, "Maria");
            Assert.IsTrue(pessoa != null);
        }

        [TestMethod]
        public void TesteProcurarPessoaInvalido()
        {
            List<Pessoa> listPessoa = new bsCoordenadas().CriarLista();

            Pessoa pessoa = new bsCoordenadas().ProcuraPessoa(listPessoa, "Teste");
            Assert.IsFalse(pessoa != null);
        }
    }
}
