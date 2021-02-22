using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Domain.Exceptions;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;
using GoltaraSolutions.Common.Domain.Report;

namespace GoltaraSolutions.SpaWeb.Domain.FuncionarioContext
{
    public sealed class FuncionarioDomainServices : DomainService<FuncionarioModel>
    {
        IFuncionarioRepository _repo = null;
        IFuncionarioReport _report = null;
        ServicoDomainServices svServico = null;
        EmpresaDomainServices svEmpresa = null;
        public FuncionarioDomainServices(IFuncionarioRepository repository, IFuncionarioReport report, ServicoDomainServices servicoDomainServices, EmpresaDomainServices empresaDomainServices)
        {
            _repo = repository;
            _report = report;
            svServico = servicoDomainServices;
            svEmpresa = empresaDomainServices;
        }
        public void Cadastrar(long idEmpresa, string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, long[] idServicos)
        {
            try
            {
                ValidarNomeJaExistente(null, idEmpresa, nome);

                EmpresaModel empresa = svEmpresa.Find(idEmpresa);

                ServicoModel servico = null;
                List<ServicoModel> servicos = null;

                if (idServicos.IsNotNull())
                {
                    servicos = new List<ServicoModel>();
                    foreach (int sId in idServicos)
                    {
                        servico = svServico.Find(sId);
                        if (servico.IsNotNull())
                        {
                            servicos.Add(servico);
                        }
                    }
                }

                FuncionarioModel f = new FuncionarioModel(empresa, nome, dataNascimento, telefone, celular, email, sexo, servicos);

                _repo.Save(f);
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
        public void Editar(long id, string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, long[] idServicos)
        {
            try
            {
                FuncionarioModel f = _repo.Find(id);
                if (f.IsNull())
                    throw new FuncionarioInvalidoException($"Funcionário {id} não encontrado.");

                ValidarNomeJaExistente(id, f.IdEmpresa, nome);

                ServicoModel servico = null;
                List<ServicoModel> servicos = null;

                if (idServicos.IsNotNull())
                {
                    servicos = new List<ServicoModel>();
                    foreach (int sId in idServicos)
                    {
                        servico = svServico.Find(sId);
                        if (servico.IsNotNull())
                        {
                            servicos.Add(servico);
                        }
                    }
                }

                f.Editar(nome, dataNascimento, telefone, celular, email, sexo, servicos);

                _repo.Save(f);
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
        public override void Delete(long id)
        {
            try
            {
                FuncionarioModel f = _repo.Find(id);
                if (f.IsNull())
                    throw new FuncionarioInvalidoException($"Funcionário {id} não encontrado.");

                f.Deletar();
                _repo.Save(f);
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
                throw new FuncionarioInvalidoException("O Nome deve ter entre 2 e 30 caracteres.");

            FuncionarioModel Funcionario = _report.Find(idEmpresa, nome);
            if (Funcionario.IsNotNull() && Funcionario.Id != id)
                throw new FuncionarioInvalidoException("Existe outro Funcionario utilizando este nome.");
        }
        public override IEnumerable<FuncionarioModel> List(long idEmpresa)
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
        public override FuncionarioModel Find(long id)
        {
            try
            {
                FuncionarioModel f = _repo.Find(id);
                return f;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override FuncionarioModel Find(long idEmpresa, string nome)
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
        public override void Recover(long id)
        {
            try
            {
                FuncionarioModel f = _repo.Find(id);
                if (f.IsNull())
                    throw new FuncionarioInvalidoException($"Funcionário {id} não encontrado.");

                f.Recuperar();
                _repo.Save(f);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
    }
}
