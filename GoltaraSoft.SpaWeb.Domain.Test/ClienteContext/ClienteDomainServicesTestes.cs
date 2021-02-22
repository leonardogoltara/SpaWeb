using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.PessoaContext.Pessoa;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Domain.Test.ClienteContext
{
    [TestClass]
    public sealed class ClienteDomainServicesTestes : TestBase
    {
        private ClienteDomainServices sv;
        private OrigemDomainServices svOrigem;
        List<string> ListaNomesToDelete = new List<string>();

        public ClienteDomainServicesTestes()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
            if (sv.IsNull()) { sv = clienteDomainServices; }
            if (svOrigem.IsNull()) { svOrigem = origemDomainServices; }

            CadastrarEmpresa();

            repoOrigem.Save(new OrigemModel(_empresa, "Indicação Teste"));
        }
        [TestCleanup]
        public void CleanUp()
        {
            ListaNomesToDelete.Add("Leo 5");
            ListaNomesToDelete.Add("Leo 4");
            ListaNomesToDelete.Add("Leo 3");
            ListaNomesToDelete.Add("Leo 2");
            ListaNomesToDelete.Add("Leo 1");
            ListaNomesToDelete.Add("DeleteCliente");
            ListaNomesToDelete.Add("RecoverCliente");
            ListaNomesToDelete.Add("Cliente_SemTelefone2");
            ListaNomesToDelete.Add("Cliente_Telefone1Valido");
            ListaNomesToDelete.Add("Leonardo");
            ListaNomesToDelete.Add("Leo Renomeado");
            ListaNomesToDelete.Add("Cliente_Telefone1Valido");


            if (ListaNomesToDelete.IsNotNull() || ListaNomesToDelete.Count > 0)
            {
                ListaNomesToDelete.ForEach(nome =>
                {
                    ClienteModel c = sv.Find(_empresa.Id, nome);
                    if (c.IsNotNull())
                        repoCliente.Delete(c.Id);
                });
            }

            OrigemModel o = svOrigem.Find(_empresa.Id, "Indicação Teste");
            if (o.IsNotNull())
                repoOrigem.Delete(o.Id);

            DeleteEmpresa();
        }
        [TestMethod]
        public void Cliente_CadastroQuandoClienteValido()
        {
            //-- Arrange
            OrigemModel origem = svOrigem.List(_empresa.Id).FirstOrDefault();

            //-- Action
            sv.Cadastrar(_empresa.Id, "Leonardo", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", origem.Id);

            //-- Asserts
            ClienteModel c = sv.Find(_empresa.Id, "Leonardo");
            Assert.IsNotNull(c);
            Assert.AreEqual("Leonardo", c.Nome);
            Assert.AreEqual("(11) 97164-5267", c.Telefone);
            Assert.AreEqual("(11) 4555-1463", c.Celular);
            Assert.AreEqual("lsgolt94@gmail.com", c.Email);
            Assert.AreEqual("M", c.Sexo);
            Assert.AreEqual(origem.Id, c.IdOrigem);

        }
        [TestMethod, ExpectedException(typeof(ClienteInvalidoException))]
        public void Cliente_CadastroQuandoNomeJaExiste()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 1", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            OrigemModel origem = svOrigem.List(_empresa.Id).FirstOrDefault();

            //-- Action
            sv.Cadastrar(_empresa.Id, "Leo 1", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", origem.Id);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(ClienteInvalidoException))]
        public void Cliente_CadastroQuandoNomeInvalido()
        {
            //-- Arrange
            OrigemModel origem = svOrigem.List(_empresa.Id).FirstOrDefault();

            //-- Action
            sv.Cadastrar(_empresa.Id, null, new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", origem.Id);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Cliente_CadastroQuandoTelefone1Valido()
        {
            //-- Arrange
            OrigemModel origem = svOrigem.List(_empresa.Id).FirstOrDefault();

            //-- Action
            sv.Cadastrar(_empresa.Id, "Cliente_Telefone1Valido", new DateTime(1994, 5, 7), null, "(11) 4555-1463", "lsgolt94@gmail.com", "M", origem.Id);

            //-- Asserts
            ClienteModel c = sv.Find(_empresa.Id, "Cliente_Telefone1Valido");
            Assert.AreEqual(c.Telefone, null);
            Assert.AreEqual(c.Celular, "(11) 4555-1463");
        }
        [TestMethod, ExpectedException(typeof(ContatoInvalidoException))]
        public void Cliente_CadastroQuandoEmailInvalido()
        {
            //-- Arrange
            OrigemModel origem = svOrigem.List(_empresa.Id).FirstOrDefault();

            //-- Action
            sv.Cadastrar(_empresa.Id, "Cliente_QuandoEmailInvalido", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", null, "M", origem.Id);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Cliente_CadastroSemTelefone2()
        {
            // Arrange
            OrigemModel origem = svOrigem.List(_empresa.Id).FirstOrDefault();

            //-- Action
            sv.Cadastrar(_empresa.Id, "Cliente_SemTelefone2", new DateTime(1994, 5, 7), "(11) 97164-5267", null, "lsgolt94@gmail.com", "M", origem.Id);

            //-- Asserts
            ClienteModel c = sv.Find(_empresa.Id, "Cliente_SemTelefone2");
            Assert.IsNotNull(c);
            Assert.AreEqual("Cliente_SemTelefone2", c.Nome);
            Assert.AreEqual("(11) 97164-5267", c.Telefone);
            Assert.IsNull(c.Celular);
            Assert.AreEqual("lsgolt94@gmail.com", c.Email);
            Assert.AreEqual(origem.Id, c.IdOrigem);
        }
        [TestMethod]
        public void Cliente_DeleteCliente()
        {
            // Arrange
            repoCliente.Save(new ClienteModel(_empresa, "DeleteCliente", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));

            //-- Action
            ClienteModel c = sv.Find(_empresa.Id, "DeleteCliente");
            sv.Delete(c.Id);
            c = sv.Find(c.Id);

            //-- Asserts
            ClienteModel cAux = sv.Find(_empresa.Id, "DeleteCliente");
            Assert.IsNotNull(cAux);
            Assert.AreEqual("DeleteCliente", cAux.Nome);
            Assert.AreEqual("(11) 97164-5267", cAux.Telefone);
            Assert.IsNotNull(cAux.Celular);
            Assert.AreEqual("lsgolt94@gmail.com", cAux.Email);
            Assert.AreEqual(c.IdOrigem, cAux.IdOrigem);
            Assert.IsTrue(cAux.Deletado);

        }
        [TestMethod]
        public void Cliente_RecoverCliente()
        {
            // Arrange
            repoCliente.Save(new ClienteModel(_empresa, "RecoverCliente", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));

            //-- Action
            ClienteModel c = sv.Find(_empresa.Id, "RecoverCliente");
            sv.Recover(c.Id);
            c = sv.Find(_empresa.Id, "RecoverCliente");

            //-- Asserts
            Assert.IsNotNull(sv.Find(_empresa.Id, "RecoverCliente"));
            Assert.IsFalse(c.Deletado);
        }
        [TestMethod]
        public void Cliente_RedefinirContatoQuandoValido()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 3", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            ClienteModel c1 = sv.Find(_empresa.Id, "Leo 3");
            string tel1 = c1.Telefone;

            //-- Act
            sv.Editar(c1.Id, c1.Nome, c1.DataNascimento, "(11) 1234-5678", "(11) 4555-1463", "lsgolt94@gmail.com", "M", c1.IdOrigem);
            ClienteModel c2 = sv.Find(_empresa.Id, "Leo 3");

            //-- Asserts
            Assert.IsNotNull(c1);
            Assert.IsNotNull(c2);
            Assert.AreEqual(c1.Id, c2.Id);
            Assert.AreNotEqual(tel1, c2.Telefone);
        }
        [TestMethod]
        public void Cliente_RedefinirContatoQuandoTelefone1Valido()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 3", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            ClienteModel c = sv.Find(_empresa.Id, "Leo 3");

            //-- Act
            sv.Editar(c.Id, c.Nome, c.DataNascimento, null, "(11) 4555-1463", "lsgolt94@gmail.com", "M", c.IdOrigem);

            //-- Asserts
            c = sv.Find(_empresa.Id, "Leo 3");
            Assert.AreEqual(c.Telefone, null);
            Assert.AreEqual(c.Celular, "(11) 4555-1463");
        }
        [TestMethod, ExpectedException(typeof(ContatoInvalidoException))]
        public void Cliente_RedefinirContatoQuandoTelefoneInvalido()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 3", new DateTime(1994, 5, 7), null, null, "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            ClienteModel c = sv.Find(_empresa.Id, "Leo 3");

            //-- Act
            sv.Editar(c.Id, c.Nome, c.DataNascimento, null, "(11) 4555-1463", "lsgolt94@gmail.com", "M", c.IdOrigem);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(ContatoInvalidoException))]
        public void Cliente_RedefinirContatoQuandoEmailInvalido()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 3", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            ClienteModel c = sv.Find(_empresa.Id, "Leo 3");

            //-- Act
            sv.Editar(c.Id, c.Nome, c.DataNascimento, "1999-2888", "(11) 4555-1463", "", "M", c.IdOrigem);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Cliente_RenomearQuandoNomeValido()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 4", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            ClienteModel c = sv.Find(_empresa.Id, "Leo 4");

            //-- Act
            sv.Editar(c.Id, "Leo Renomeado", c.DataNascimento, c.Telefone, c.Celular, c.Email, c.Sexo, c.IdOrigem);
            ClienteModel c2 = sv.Find(c.Id);

            //-- Asserts
            Assert.IsNotNull(c2);
            Assert.AreEqual("Leo Renomeado", c2.Nome);
        }
        [TestMethod, ExpectedException(typeof(ClienteInvalidoException))]
        public void Cliente_RenomearQuandoNomeInvalido()
        {
            //-- Arrange
            repoCliente.Save(new ClienteModel(_empresa, "Leo 4", new DateTime(1994, 5, 7), "(11) 97164-5267", "(11) 4555-1463", "lsgolt94@gmail.com", "M", svOrigem.List(_empresa.Id).FirstOrDefault()));
            ClienteModel c = sv.Find(_empresa.Id, "Leo 4");

            //-- Act
            sv.Editar(c.Id, null, c.DataNascimento, c.Telefone, c.Celular, c.Email, c.Sexo, c.IdOrigem);

            //-- Asserts
            Assert.IsNotNull(sv.Find(_empresa.Id, "Leo Renomeado"));
        }
        [TestMethod]
        public void Cliente_ListarTodos()
        {
            //-- Assert
            Assert.IsNotNull(sv.List(_empresa.Id));
        }
    }
}
