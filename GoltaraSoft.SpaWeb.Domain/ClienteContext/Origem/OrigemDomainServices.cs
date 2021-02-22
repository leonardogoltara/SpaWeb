using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Domain.Exceptions;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    public class OrigemDomainServices : DomainService<OrigemModel>
    {
        IOrigemRepository _repository = null;
        IOrigemReport _report = null;
        EmpresaDomainServices svEmpresa = null;
        public OrigemDomainServices(IOrigemRepository repository, IOrigemReport report, EmpresaDomainServices empresaDomainServices)
        {
            _repository = repository;
            _report = report;
            svEmpresa = empresaDomainServices;
        }
        public void Cadastrar(long idEmpresa, string nome)
        {
            try
            {
                ValidarNomeJaExistente(null, idEmpresa, nome);

                EmpresaModel empresa = svEmpresa.Find(idEmpresa);

                OrigemModel Origem = new OrigemModel(empresa, nome);

                _repository.Save(Origem);
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
        public void Editar(long id, string nome)
        {
            try
            {
                OrigemModel origem = Find(id);
                if (origem.IsNull())
                    throw new OrigemInvalidoException($"Origem {id} não encontrado.");

                ValidarNomeJaExistente(id, origem.IdEmpresa, nome);

                origem.Editar(nome);

                _repository.Save(origem);
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
                throw new OrigemInvalidoException("O Nome deve ter entre 2 e 30 caracteres.");


            OrigemModel Origem = _report.Find(idEmpresa, nome);
            if (Origem.IsNotNull() && Origem.Id != id)
                throw new OrigemInvalidoException("Existe outra origem utilizando este nome.");
        }
        public override void Delete(long id)
        {
            try
            {
                OrigemModel origem = _repository.Find(id);
                if (origem.IsNull())
                    throw new OrigemInvalidoException($"Origem {id} não encontrado.");

                origem.Deletar();
                _repository.Save(origem);
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
                OrigemModel origem = _repository.Find(id);
                if (origem.IsNull())
                    throw new OrigemInvalidoException($"Origem {id} não encontrado.");

                origem.Recuperar();
                _repository.Save(origem);
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
        public override OrigemModel Find(long id)
        {
            try
            {
                return _repository.Find(id);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public override OrigemModel Find(long idEmpresa, string nome)
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
        public override IEnumerable<OrigemModel> List(long idEmpresa)
        {
            try
            {
                return _report.List(idEmpresa)
                    .OrderBy(x => x.Nome)
                    .ThenBy(x => x.Deletado);
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
    }
}
