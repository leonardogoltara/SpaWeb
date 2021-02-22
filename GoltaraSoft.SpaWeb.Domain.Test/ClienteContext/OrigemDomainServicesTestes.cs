using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.Common.Extensions;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.Test.ClienteContext
{
    [TestClass]
    public sealed class OrigemDomainServicesTestes : TestBase
    {
        private OrigemDomainServices sv;
        List<string> ListaNomesToDelete = new List<string>();

        public OrigemDomainServicesTestes()
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            if (sv.IsNull()) { sv = origemDomainServices; }

            CadastrarEmpresa();
        }
        [TestCleanup]
        public void CleanUp()
        {
            ListaNomesToDelete.Add("Produto 1");
            ListaNomesToDelete.Add("Origem 1");
            ListaNomesToDelete.Add("Origem 2");
            ListaNomesToDelete.Add("Origem 3");
            ListaNomesToDelete.Add("Origem 4");
            ListaNomesToDelete.Add("Origem 5");
            ListaNomesToDelete.Add("Origem 6");
            ListaNomesToDelete.Add("Origem Renomeado");
            ListaNomesToDelete.Add("Nova Origem");
            ListaNomesToDelete.Add("Origem Recup.");

            if (ListaNomesToDelete.IsNotNull() || ListaNomesToDelete.Count > 0)
            {
                ListaNomesToDelete.ForEach(nome =>
                {
                    OrigemModel o = sv.Find(_empresa.Id, nome);
                    if (o.IsNotNull())
                        repoOrigem.Delete(o.Id);
                });
            }

            DeleteEmpresa();
        }
        [TestMethod]
        public void Origem_CadastroQuandoOrigemValido()
        {
            //-- Arrange

            string nomeOrigem = "Nova Origem";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeOrigem);

            //-- Asserts
            OrigemModel o = sv.Find(_empresa.Id, nomeOrigem);
            Assert.IsNotNull(o);
            Assert.AreEqual(nomeOrigem, o.Nome);
            Assert.IsNotNull(sv.List(_empresa.Id));

        }
        [TestMethod, ExpectedException(typeof(OrigemInvalidoException))]
        public void Origem_CadastroQuandoOrigemTemNomeInvalido()
        {
            //-- Arrange

            string nomeOrigem = string.Empty;

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeOrigem);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(OrigemInvalidoException))]
        public void Origem_CadastroQuandoOrigemNomeJaExiste()
        {
            //-- Arrange

            string nomeOrigem = "Origem 3";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeOrigem);
            sv.Cadastrar(_empresa.Id, nomeOrigem);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(OrigemInvalidoException))]
        public void Origem_RenomearOrigemQuandoNomeJaExiste()
        {
            //-- Arrange
            repoOrigem.Save(new OrigemModel(_empresa, "Origem 1"));
            repoOrigem.Save(new OrigemModel(_empresa, "Origem 2"));
            string nomeOrigem = "Origem 2";

            //-- Act
            OrigemModel s = sv.Find(_empresa.Id, "Origem 1");
            sv.Editar(s.Id, nomeOrigem);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(OrigemInvalidoException))]
        public void Origem_RenomearOrigemQuandoNomeInvalido()
        {
            //-- Arrange
            repoOrigem.Save(new OrigemModel(_empresa, "Origem 3"));

            string nomeOrigem1 = "Origem 3";
            string nomeOrigem2 = null;
            OrigemModel s = sv.Find(_empresa.Id, nomeOrigem1);

            //-- Asserts
            Assert.IsNotNull(s);

            //-- Arrange
            sv.Editar(s.Id, nomeOrigem2);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Origem_RenomearOrigemQuandoNomeValido()
        {
            //-- Arrange
            repoOrigem.Save(new OrigemModel(_empresa, "Origem 4"));
            string nomeOrigem1 = "Origem 4";
            string nomeOrigem2 = "Origem Renomeado";
            OrigemModel s = sv.Find(_empresa.Id, nomeOrigem1);


            //-- Act
            sv.Editar(s.Id, nomeOrigem2);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsNotNull(sv.Find(s.Id));
            Assert.IsNotNull(sv.Find(_empresa.Id, nomeOrigem2));
            Assert.IsNull(sv.Find(_empresa.Id, nomeOrigem1));
            Assert.AreEqual(nomeOrigem2, s.Nome);
        }
        [TestMethod]
        public void Origem_DeletarOrigem()
        {
            //-- Arrange
            repoOrigem.Save(new OrigemModel(_empresa, "Origem 6"));
            string nomeOrigem = "Origem 6";
            OrigemModel s = sv.Find(_empresa.Id, nomeOrigem);

            //-- Act
            sv.Delete(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(s.Deletado);
        }
        [TestMethod]
        public void Origem_RecoverOrigem()
        {
            //-- Arrange
            repoOrigem.Save(new OrigemModel(_empresa, "Origem Recup."));

            string nomeOrigem = "Origem Recup.";
            OrigemModel s = sv.Find(_empresa.Id, nomeOrigem);
            sv.Delete(s.Id);
            s = sv.Find(_empresa.Id, nomeOrigem);
            bool deletado = s.Deletado;

            //-- Act
            sv.Recover(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(deletado);
            Assert.IsFalse(s.Deletado);
        }
        [TestMethod]
        public void Origem_ListarTodos()
        {
            //-- Assert
            Assert.IsNotNull(sv.List(_empresa.Id));
        }
    }
}
