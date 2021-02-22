using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Domain.Exceptions;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ProdutoContext
{
    public sealed class ProdutoDomainServices : DomainService<ProdutoModel>
    {
        IProdutoRepository _repo = null;
        IProdutoReport _report = null;
        EmpresaDomainServices svEmpresa = null;

        public ProdutoDomainServices(IProdutoRepository repository, IProdutoReport report, EmpresaDomainServices empresaDomainServices)
        {
            _repo = repository;
            _report = report;
            svEmpresa = empresaDomainServices;
        }
        public void Cadastrar(long idEmpresa, string nome, decimal preco)
        {
            try
            {
                ValidarNomeJaExistente(null, idEmpresa, nome);

                EmpresaModel empresa = svEmpresa.Find(idEmpresa);

                ProdutoModel Produto = new ProdutoModel(empresa, nome, preco);

                _repo.Save(Produto);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public void Editar(long id, string nome, decimal preco)
        {
            try
            {
                ProdutoModel p = Find(id);
                if (p.IsNull())
                    throw new ProdutoInvalidoException($"Produto {id} não encontrado.");

                ValidarNomeJaExistente(id, p.IdEmpresa, nome);

                p.Editar(nome, preco);

                _repo.Save(p);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        private void ValidarNomeJaExistente(long? id, long idEmpresa, string nome)
        {
            if (!nome.LengthValid(false, 2, 30))
                throw new ProdutoInvalidoException("O Nome deve ter entre 2 e 30 caracteres.");

            ProdutoModel Produto = _report.Find(idEmpresa, nome);
            if (Produto.IsNotNull() && Produto.Id != id)
                throw new ProdutoInvalidoException("Existe outro produto utilizando este nome.");
        }
        public override void Delete(long id)
        {
            try
            {
                ProdutoModel Produto = _repo.Find(id);
                if (Produto.IsNull())
                    throw new ProdutoInvalidoException($"Produto {id} não encontrado.");

                Produto.Deletar();
                _repo.Save(Produto);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override void Recover(long id)
        {
            try
            {
                ProdutoModel Produto = _repo.Find(id);
                if (Produto.IsNull())
                    throw new ProdutoInvalidoException($"Produto {id} não encontrado.");

                Produto.Recuperar();
                _repo.Save(Produto);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override ProdutoModel Find(long id)
        {
            try
            {
                return _repo.Find(id);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override ProdutoModel Find(long idEmpresa, string nome)
        {
            try
            {
                return _report.Find(idEmpresa, nome);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override IEnumerable<ProdutoModel> List(long idEmpresa)
        {
            try
            {
                return _report.List(idEmpresa);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public IEnumerable<FiltrosReportView> ListAtivos(long idEmpresa)
        {
            try
            {
                return _report.ListarFiltros(idEmpresa, false);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
    }
}
