using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.Common.Extensions;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.Test.ProdutoContext
{
    [TestClass]
    public sealed class ProdutoDomainServicesTestes : TestBase
    {
        private ProdutoDomainServices sv;
        List<string> ListaNomesToDelete = new List<string>();

        public ProdutoDomainServicesTestes()
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            if (sv.IsNull()) { sv = produtoDomainServices; }

            CadastrarEmpresa();
        }
        [TestCleanup]
        public void CleanUp()
        {
            ListaNomesToDelete.Add("Produto 1");
            ListaNomesToDelete.Add("Produto 2");
            ListaNomesToDelete.Add("Produto 3");
            ListaNomesToDelete.Add("Produto 4");
            ListaNomesToDelete.Add("Produto 5");
            ListaNomesToDelete.Add("Produto 6");
            ListaNomesToDelete.Add("Produto 7");
            ListaNomesToDelete.Add("Produto 8");
            ListaNomesToDelete.Add("Produto Renomeado");
            ListaNomesToDelete.Add("Produto Novo");
            ListaNomesToDelete.Add("Produto Recuperacao");


            if (ListaNomesToDelete.IsNotNull() || ListaNomesToDelete.Count > 0)
            {
                ListaNomesToDelete.ForEach(nome =>
                {
                    ProdutoModel o = sv.Find(_empresa.Id, nome);
                    if (o.IsNotNull())
                        repoProduto.Delete(o.Id);
                });
            }

            DeleteEmpresa();
        }
        [TestMethod]
        public void Produto_CadastroQuandoProdutoValido()
        {
            //-- Arrange
            string nomeProduto = "Produto Novo";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeProduto, 25);

            //-- Asserts
            Assert.IsNotNull(sv.Find(_empresa.Id, nomeProduto));
            Assert.IsNotNull(sv.List(_empresa.Id));

        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_CadastroQuandoPrecoZero()
        {
            //-- Arrange
            string nomeProduto = "Produto Novo";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeProduto, 0);

            //-- Asserts
            Assert.IsNotNull(sv.Find(_empresa.Id, nomeProduto));
            Assert.IsNotNull(sv.List(_empresa.Id));

        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_CadastroQuandoPrecoMenorZero()
        {
            //-- Arrange
            string nomeProduto = "Produto Novo";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeProduto, -2);

            //-- Asserts
            Assert.IsNotNull(sv.Find(_empresa.Id, nomeProduto));
            Assert.IsNotNull(sv.List(_empresa.Id));

        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_CadastroQuandoProdutoTemNomeInvalido()
        {
            //-- Arrange
            string nomeProduto = string.Empty;

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeProduto, 25);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_CadastroQuandoProdutoNomeJaExiste()
        {
            //-- Arrange
            string nomeProduto = "Produto 7";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeProduto, 25);
            sv.Cadastrar(_empresa.Id, nomeProduto, 25);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_CadastroQuandoProdutoTemPrecoInvalido()
        {
            //-- Arrange
            string nomeProduto = "Produto 8";

            //-- Act
            sv.Cadastrar(_empresa.Id, nomeProduto, -2);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_RenomearProdutoQuandoNomeJaExiste()
        {
            //-- Arrange
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 1", 40));
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 2", 40));
            string nomeProduto = "Produto 2";

            //-- Act
            ProdutoModel s = sv.Find(_empresa.Id, "Produto 1");
            sv.Editar(s.Id, nomeProduto, s.Preco);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_RenomearProdutoQuandoNomeInvalido()
        {
            //-- Arrange
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 3", 40));
            string nomeProduto1 = "Produto 3";
            string nomeProduto2 = null;
            ProdutoModel s = sv.Find(_empresa.Id, nomeProduto1);

            //-- Asserts
            Assert.IsNotNull(s);

            //-- Arrange
            sv.Editar(s.Id, nomeProduto2, s.Preco);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Produto_RenomearProdutoQuandoNomeValido()
        {
            //-- Arrange
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 4", 40));
            string nomeProduto1 = "Produto 4";
            string nomeProduto2 = "Produto Renomeado";
            ProdutoModel s = sv.Find(_empresa.Id, nomeProduto1);


            //-- Act
            sv.Editar(s.Id, nomeProduto2, s.Preco);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsNotNull(sv.Find(s.Id));
            Assert.IsNotNull(sv.Find(_empresa.Id, nomeProduto2));
            Assert.IsNull(sv.Find(_empresa.Id, nomeProduto1));
            Assert.AreEqual(nomeProduto2, s.Nome);
        }
        [TestMethod, ExpectedException(typeof(ProdutoInvalidoException))]
        public void Produto_RedefinirPrecoQuandoPrecoInvalido()
        {
            //-- Arrange
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 5", 40));
            string nomeProduto1 = "Produto 5";
            decimal precoNovo = -2;
            ProdutoModel s = sv.Find(_empresa.Id, nomeProduto1);

            //-- Act
            sv.Editar(s.Id, s.Nome, precoNovo);

            //-- Asserts
            Assert.Inconclusive();
        }
        [TestMethod]
        public void Produto_RedefinirPrecoQuandoPrecoValido()
        {
            //-- Arrange
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 5", 40));
            string nomeProduto1 = "Produto 5";
            decimal precoNovo = 55;
            ProdutoModel s = sv.Find(_empresa.Id, nomeProduto1);

            //-- Act
            sv.Editar(s.Id, s.Nome, precoNovo);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsNotNull(s);
            Assert.AreEqual(precoNovo, s.Preco);

        }
        [TestMethod]
        public void Produto_DeletarProduto()
        {
            //-- Arrange
            repoProduto.Save(new ProdutoModel(_empresa, "Produto 6", 40));
            string nomeProduto = "Produto 6";
            ProdutoModel s = sv.Find(_empresa.Id, nomeProduto);

            //-- Act
            sv.Delete(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(s.Deletado);
        }
        [TestMethod]
        public void Produto_RecoverProduto()
        {
            //-- Arrange
            string nomeProduto = "Produto Recuperacao";
            repoProduto.Save(new ProdutoModel(_empresa, nomeProduto, 40));
            ProdutoModel s = sv.Find(_empresa.Id, nomeProduto);
            sv.Delete(s.Id);
            s = sv.Find(_empresa.Id, nomeProduto);
            bool deletado = s.Deletado;

            //-- Act
            sv.Recover(s.Id);
            s = sv.Find(s.Id);

            //-- Asserts
            Assert.IsTrue(deletado);
            Assert.IsFalse(s.Deletado);
        }
        [TestMethod]
        public void Produto_ListarTodos()
        {
            //-- Assert
            Assert.IsNotNull(sv.List(_empresa.Id));
        }
    }
}
