using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System.Collections.Generic;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Domain.Test.ServicoContext
{
    [TestClass]
    public sealed class ServicoDomainServicesTestes : TestBase
    {
        ServicoDomainServices sv = null;
        List<string> ListaNomesToDelete = new List<string>();

        public ServicoDomainServicesTestes()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
            if (sv.IsNull()) { sv =servicoDomainServices; }

            CadastrarEmpresa();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ListaNomesToDelete.Add("Serviço 1");
            ListaNomesToDelete.Add("Serviço 1");
            ListaNomesToDelete.Add("Serviço 2");
            ListaNomesToDelete.Add("Serviço 3");
            ListaNomesToDelete.Add("Serviço 4");
            ListaNomesToDelete.Add("Serviço 5");
            ListaNomesToDelete.Add("Serviço 6");
            ListaNomesToDelete.Add("Serviço 7");
            ListaNomesToDelete.Add("Serviço 8");
            ListaNomesToDelete.Add("Serviço Novo");
            ListaNomesToDelete.Add("Corte de Cabelo");
            ListaNomesToDelete.Add("Serviço Recuperacao");


            if (ListaNomesToDelete.IsNotNull() || ListaNomesToDelete.Count > 0)
            {
                ListaNomesToDelete.ForEach(nome =>
                {
                    ServicoModel o = sv.Find(_empresa.Id, nome);
                    if (o.IsNotNull())
                        repoServico.Delete(o.Id);
                });
            }

            DeleteEmpresa();
        }

        [TestMethod]
        public void Servico_Cadastro_Success()
        {
            //-- Arrange
            string nomeServico = "Serviço Novo";
            decimal preco = 25;
            bool precoFixo = true;

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeServico, preco, precoFixo);

            //-- Asserts
            ServicoModel servico = sv.Find(_empresa.Id, nomeServico);

            Assert.IsNotNull(servico);
            Assert.AreEqual(nomeServico, servico.Nome);
            Assert.AreEqual(preco, servico.Preco);
            Assert.AreEqual(precoFixo, servico.PrecoFixo);

        }

        [TestMethod]
        public void Servico_Editar_Success()
        {
            //-- Arrange
            string nomeServico = "Serviço Novo";
            decimal preco = 25;
            bool precoFixo = true;
            sv.Cadastrar(_empresa.Id, nomeServico, preco, precoFixo);
            ServicoModel s = sv.Find(_empresa.Id, nomeServico);

            //-- Act
            string novoNomeServico = "Serviço 8";
            decimal novoPreco = 25;
            bool novoPrecoFixo = true;
            sv.Editar(s.Id, novoNomeServico, novoPreco, novoPrecoFixo);

            //-- Asserts
            ServicoModel servico = sv.Find(s.Id);

            Assert.IsNotNull(servico);
            Assert.AreEqual(novoNomeServico, servico.Nome);
            Assert.AreEqual(novoPreco, servico.Preco);
            Assert.AreEqual(novoPrecoFixo, servico.PrecoFixo);
        }
        [TestMethod]
        public void Servico_RedefinirPreco_Success()
        {
            //-- Arrange
            string nomeServico1 = "Serviço 5";
            bool precoFico = true;
            repoServico.Save(new ServicoModel(_empresa, nomeServico1, 50, precoFico));

            decimal precoNovo = 55;
            ServicoModel s = sv.Find(_empresa.Id, nomeServico1);

            //-- Act
            sv.Editar(s.Id, s.Nome, precoNovo, s.PrecoFixo);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsNotNull(s);
            Assert.AreEqual(nomeServico1, s.Nome);
            Assert.AreEqual(precoNovo, s.Preco);
            Assert.AreEqual(precoFico, s.PrecoFixo);

        }
        [TestMethod]
        public void Servico_Deletar_Success()
        {
            //-- Arrange
            repoServico.Save(new ServicoModel(_empresa, "Serviço 6", 50, false));
            string nomeServico = "Serviço 6";
            ServicoModel s = sv.Find(_empresa.Id, nomeServico);

            //-- Act
            sv.Delete(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(s.Deletado);
        }
        [TestMethod]
        public void Servico_Recover_Success()
        {
            //-- Arrange
            repoServico.Save(new ServicoModel(_empresa, "Serviço Recuperacao", 50, false));
            string nomeServico = "Serviço Recuperacao";
            ServicoModel s = sv.Find(_empresa.Id, nomeServico);
            sv.Delete(s.Id);
            s = sv.Find(_empresa.Id, nomeServico);
            bool deletado = s.Deletado;

            //-- Act
            sv.Recover(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(deletado);
            Assert.IsFalse(s.Deletado);
        }
        [TestMethod]
        public void Servico_ListarTodos_Success()
        {
            //-- Assert
            Assert.IsNotNull(sv.List(_empresa.Id));
        }
    }
}
