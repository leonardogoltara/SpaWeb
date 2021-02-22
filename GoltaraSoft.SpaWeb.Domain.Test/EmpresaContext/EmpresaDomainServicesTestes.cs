using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Collections.Generic;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Domain.Test.EmpresaContext
{
    [TestClass]
    public sealed class EmpresaDomainServicesTestes : TestBase
    {
        EmpresaDomainServices sv = null;
        List<string> ListaNomesToDelete = new List<string>();

        public EmpresaDomainServicesTestes()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
            if (sv.IsNull()) { sv = empresaDomainServices; }

            CadastrarEmpresa();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ListaNomesToDelete.Add("15752060000138");
            ListaNomesToDelete.Add("21551861000138");
            ListaNomesToDelete.Add("67436845000103");
            ListaNomesToDelete.Add("25912083000116");

            if (ListaNomesToDelete.IsNotNull() || ListaNomesToDelete.Count > 0)
            {
                ListaNomesToDelete.ForEach(cnpj =>
                {
                    _empresa = sv.FindCNPJ(cnpj);
                    if (_empresa != null)
                        DeleteEmpresa();
                });
            }
        }

        [TestMethod]
        public void Empresa_Cadastro_Success()
        {
            //-- Arrange
            string nomeEmpresa = "Empresa Novo";
            string cnpj = "15752060000138";
            string nomeResponsavel = "Leonardo Goltara";
            string telefoneResponsavel = "(11) 97164-5267";
            string emailResponsavel = "leo.goltara@gmail.com";


            //-- Act
            sv.Cadastrar(cnpj, nomeEmpresa, nomeResponsavel, telefoneResponsavel, emailResponsavel);

            //-- Asserts
            EmpresaModel Empresa = sv.FindCNPJ(cnpj);

            Assert.IsNotNull(Empresa);
            Assert.AreEqual(nomeEmpresa, Empresa.Nome);
            Assert.AreEqual(cnpj, Empresa.CNPJ);
            Assert.AreEqual(nomeResponsavel, Empresa.ResponsavelCobranca.Nome);
            Assert.AreEqual(telefoneResponsavel, Empresa.ResponsavelCobranca.Telefone);
            Assert.AreEqual(emailResponsavel, Empresa.ResponsavelCobranca.Email);
        }
        [TestMethod]
        public void Empresa_Editar_Success()
        {
            //-- Arrange
            string nomeEmpresa = "Empresa Novo";
            string cnpj = "21551861000138";
            string nomeResponsavel = "Leonardo Goltara";
            string telefoneResponsavel = "(11) 97164-5267";
            string emailResponsavel = "leo.goltara@gmail.com";

            sv.Cadastrar(cnpj, nomeEmpresa, nomeResponsavel, telefoneResponsavel, emailResponsavel);
            EmpresaModel e = sv.FindCNPJ(cnpj);

            //-- Act
            string novoNomeEmpresa = "Empresa Alterada";
            string novoCNPJ = "67436845000103";
            string novoNomeResponsavel = "Leonardo Souza";
            string novoTelefoneResponsavel = "(11) 97164-4444";
            string novoEmailResponsavel = "leo.souza@gmail.com";

            sv.Editar(e.Id, novoCNPJ, novoNomeEmpresa, novoNomeResponsavel, novoTelefoneResponsavel, novoEmailResponsavel);

            //-- Asserts
            EmpresaModel Empresa = sv.Find(e.Id);

            Assert.IsNotNull(Empresa);
            Assert.AreEqual(novoNomeEmpresa, Empresa.Nome);
            Assert.AreEqual(novoCNPJ, Empresa.CNPJ);
            Assert.AreEqual(novoNomeResponsavel, Empresa.ResponsavelCobranca.Nome);
            Assert.AreEqual(novoTelefoneResponsavel, Empresa.ResponsavelCobranca.Telefone);
            Assert.AreEqual(novoEmailResponsavel, Empresa.ResponsavelCobranca.Email);
        }
        [TestMethod]
        public void Empresa_Deletar_Success()
        {
            //-- Arrange
            string nomeEmpresa = "Empresa 6";
            string cnpj = "15752060000138";
            string nomeResponsavel = "Leonardo Goltara";
            string telefoneResponsavel = "(11) 97164-5267";
            string emailResponsavel = "leo.goltara@gmail.com";

            repoEmpresa.Save(new EmpresaModel(cnpj, nomeEmpresa, nomeResponsavel, telefoneResponsavel, emailResponsavel));
            EmpresaModel s = sv.FindCNPJ(cnpj);

            //-- Act
            sv.Delete(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(s.Deletado);
        }
        [TestMethod]
        public void Empresa_Recover_Success()
        {
            //-- Arrange
            string nomeEmpresa = "Empresa Recuperacao";
            string cnpj = "15752060000138";
            string nomeResponsavel = "Leonardo Goltara";
            string telefoneResponsavel = "(11) 97164-5267";
            string emailResponsavel = "leo.goltara@gmail.com";

            repoEmpresa.Save(new EmpresaModel(cnpj, nomeEmpresa, nomeResponsavel, telefoneResponsavel, emailResponsavel));
            EmpresaModel s = sv.FindCNPJ(cnpj);

            sv.Delete(s.Id);
            s = sv.FindCNPJ(cnpj);
            bool deletado = s.Deletado;

            //-- Act
            sv.Recover(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(deletado);
            Assert.IsFalse(s.Deletado);
        }
        [TestMethod]
        public void Empresa_ListarTodos_Success()
        {
            //-- Assert
            Assert.IsNotNull(sv.List());
        }
    }
}
