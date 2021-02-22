using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Domain.Exceptions;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ServicoContext
{
    public sealed class ServicoDomainServices : DomainService<ServicoModel>
    {
        IServicoRepository _repo = null;
        IServicoReport _report = null;
        EmpresaDomainServices svEmpresa = null;
        public ServicoDomainServices(IServicoRepository repository, IServicoReport report, EmpresaDomainServices empresaDomainServices)
        {
            _repo = repository;
            _report = report;
            svEmpresa = empresaDomainServices;
        }
        public void Cadastrar(long idEmpresa, string nome, decimal preco, bool precoFixo)
        {
            try
            {
                ValidarNomeJaExistente(null, idEmpresa, nome);

                EmpresaModel empresa = svEmpresa.Find(idEmpresa);

                ServicoModel servico = new ServicoModel(empresa, nome, preco, precoFixo);

                _repo.Save(servico);
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
        public void Editar(long id, string nome, decimal preco, bool precoFixo)
        {
            try
            {
                ServicoModel servico = Find(id);
                if (servico.IsNull())
                    throw new ServicoInvalidoException($"Serviço {id} não encontrado.");

                ValidarNomeJaExistente(id, servico.IdEmpresa, nome);

                servico.Editar(nome, preco, precoFixo);

                _repo.Save(servico);
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
                throw new ServicoInvalidoException("O Nome deve ter entre 2 e 30 caracteres.");

            ServicoModel servico = _report.Find(idEmpresa, nome);
            if (servico.IsNotNull() && servico.Id != id)
                throw new ServicoInvalidoException("Existe outro serviço utilizando este nome.");
        }
        public override void Delete(long id)
        {
            try
            {
                ServicoModel servico = _repo.Find(id);
                if (servico.IsNull())
                    throw new ServicoInvalidoException($"Serviço {id} não encontrado.");

                servico.Deletar();
                _repo.Save(servico);
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
                ServicoModel servico = _repo.Find(id);
                if (servico.IsNull())
                    throw new ServicoInvalidoException($"Serviço {id} não encontrado.");

                servico.Recuperar();
                _repo.Save(servico);
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
        public override ServicoModel Find(long id)
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
        public override ServicoModel Find(long idEmpresa, string nome)
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
        public IEnumerable<FiltrosReportView> FindByFuncionario(long idEmpresa, long idFuncionario)
        {
            try
            {
                return _report.FindByFuncionario(idEmpresa, idFuncionario);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override IEnumerable<ServicoModel> List(long idEmpresa)
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
        public IEnumerable<FiltrosReportView> ListarFiltros(long idEmpresa)
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
