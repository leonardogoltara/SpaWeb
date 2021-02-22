using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Domain.Exceptions;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    public sealed class ClienteDomainServices : DomainService<ClienteModel>
    {
        IClienteRepository _repo = null;
        IClienteReport _report = null;
        OrigemDomainServices svOrigem = null;
        EmpresaDomainServices svEmpresa = null;

        public ClienteDomainServices(IClienteRepository repository,
            IClienteReport report,
            OrigemDomainServices origemDomainServices,
            EmpresaDomainServices empresaDomainServices)
        {
            _repo = repository;
            _report = report;
            svOrigem = origemDomainServices;
            svEmpresa = empresaDomainServices;
        }

        public void Cadastrar(long idEmpresa, string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, long idOrigem)
        {
            try
            {
                ValidarNomeJaExistente(null, idEmpresa, nome);

                OrigemModel o = svOrigem.Find(idOrigem);

                if (o.IsNull())
                    throw new OrigemInvalidoException("Canal de divulgação inválido.");

                EmpresaModel empresa = svEmpresa.Find(idEmpresa);

                ClienteModel c = new ClienteModel(empresa, nome, dataNascimento, telefone, celular, email, sexo, o);

                _repo.Save(c);
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
        public void Editar(long id, string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, long idOrigem)
        {
            try
            {
                ClienteModel c = _repo.Find(id);

                if (c.IsNull())
                    throw new ClienteInvalidoException($"Cliente {id} não encontrado.");

                ValidarNomeJaExistente(id, c.IdEmpresa, nome);

                OrigemModel o = svOrigem.Find(idOrigem);

                if (o.IsNull())
                    throw new ClienteInvalidoException("Origem inválido.");

                c.Editar(nome, dataNascimento, telefone, celular, email, sexo, o);

                _repo.Save(c);
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
                ClienteModel c = _repo.Find(id);

                if (c.IsNull())
                    throw new ClienteInvalidoException($"Cliente {id} não encontrado.");

                c.Deletar();
                _repo.Save(c);
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
                throw new ClienteInvalidoException("O Nome deve ter entre 2 e 30 caracteres.");

            ClienteModel cliente = _report.Find(idEmpresa, nome);

            if (!cliente.IsNull() && cliente.Id != id)
                throw new ClienteInvalidoException("Existe outro Cliente utilizando este nome.");
        }
        public override IEnumerable<ClienteModel> List(long idEmpresa)
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
        public IEnumerable<FiltrosReportView> ListarFiltro(long idEmpresa)
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
        public override ClienteModel Find(long id)
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
        public override ClienteModel Find(long idEmpresa, string nome)
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
                ClienteModel c = _repo.Find(id);

                if (c.IsNull())
                    throw new ClienteInvalidoException($"Cliente {id} não encontrado.");

                c.Recuperar();
                _repo.Save(c);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public List<AniversarianteReportView> AniversariantesMes(long idEmpresa)
        {
            try
            {
                List<AniversarianteReportView> aniversariantes = _report.AniversariantesMes(idEmpresa).ToList();

                return aniversariantes;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
    }
}
