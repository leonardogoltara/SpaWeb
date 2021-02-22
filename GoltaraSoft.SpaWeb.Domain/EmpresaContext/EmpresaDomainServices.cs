using GoltaraSolutions.Common.Domain.Exceptions;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Domain.EmpresaContext
{
    public sealed class EmpresaDomainServices //: DomainService<EmpresaModel>
    {
        IEmpresaRepository _repo = null;
        IEmpresaReport _report = null;

        OrigemDomainServices _origemDomainServices;
        ServicoDomainServices _servicoDomainServices;
        ProdutoDomainServices _produtoDomainServices;
        ClienteDomainServices _clienteDomainServices;
        FuncionarioDomainServices _funcionarioDomainServices;

        public EmpresaDomainServices(IEmpresaRepository repository, IEmpresaReport report,
            IOrigemRepository origemRepository, IOrigemReport origemReport,
            IServicoRepository servicoRepository, IServicoReport servicoReport,
            IProdutoRepository produtoRepository, IProdutoReport produtoReport,
            IClienteRepository clienteRepository, IClienteReport clienteReport,
            IFuncionarioRepository funcionarioRepository, IFuncionarioReport funcionarioReport)
        {
            _repo = repository;
            _report = report;

            _origemDomainServices = new OrigemDomainServices(origemRepository, origemReport, this);
            _servicoDomainServices = new ServicoDomainServices(servicoRepository, servicoReport, this);
            _produtoDomainServices = new ProdutoDomainServices(produtoRepository, produtoReport, this);
            _clienteDomainServices = new ClienteDomainServices(clienteRepository, clienteReport, _origemDomainServices, this);
            _funcionarioDomainServices = new FuncionarioDomainServices(funcionarioRepository, funcionarioReport, _servicoDomainServices, this);

        }

        public void Cadastrar(string cnpj, string nome, string nomeResponsavel, string telefoneResponsavel, string emailResponsavel)
        {
            try
            {
                ValidarNomeJaExistente(null, nome);
                EmpresaModel Empresa = new EmpresaModel(cnpj,
                    nome,
                    nomeResponsavel,
                    telefoneResponsavel,
                    emailResponsavel);

                _repo.Save(Empresa);

                SetupInicial(Empresa.Id);
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
        public void Editar(long id, string cnpj, string nome, string nomeResponsavel, string telefoneResponsavel, string emailResponsavel)
        {
            try
            {
                ValidarNomeJaExistente(id, nome);
                EmpresaModel empresa = Find(id);
                if (empresa.IsNull())
                    throw new EmpresaInvalidoException($"Empresa {id} não encontrado.");

                empresa.Editar(cnpj, nome, nomeResponsavel, telefoneResponsavel, emailResponsavel);

                _repo.Save(empresa);
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
        public void SetupInicial(long idEmpresa)
        {
            try
            {
                try
                {
                    // Origem
                    _origemDomainServices.Cadastrar(idEmpresa, "Panfletagem");
                    _origemDomainServices.Cadastrar(idEmpresa, "Outdoors");
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

                try
                {
                    // Serviço
                    _servicoDomainServices.Cadastrar(idEmpresa, "Corte Feminino", 30, false);
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

                try
                {
                    // Produto
                    _produtoDomainServices.Cadastrar(idEmpresa, "Creme hidratante", 20);
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

                try
                {
                    // Cliente
                    long idOrigem = 0;
                    IEnumerable<OrigemModel> origens = _origemDomainServices.List(idEmpresa);
                    if (origens.IsNotNull() && origens.Count() > 0)
                        idOrigem = origens.FirstOrDefault().Id;

                    _clienteDomainServices.Cadastrar(idEmpresa, "Renata (Exemplo)", new DateTime(1991, 02, 05), null, "(11) 97164-5264", "renata.dias@exemplo.com.br", "F", idOrigem);
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

                try
                {
                    // Funcionário
                    long idServico = 0;
                    IEnumerable<ServicoModel> origens = _servicoDomainServices.List(idEmpresa);
                    if (origens.IsNotNull() && origens.Count() > 0)
                        idServico = origens.FirstOrDefault().Id;

                    _funcionarioDomainServices.Cadastrar(idEmpresa, "Gustavo (Exemplo)", new DateTime(1985, 02, 05), "(11) 4665-5849", null, "gustavo@exemplo.com.br", "F", new long[] { idServico });
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
            catch (DomainException)
            {
                //Logger.Log.Warn(dEx);
            }
            catch (Exception)
            {
                //Logger.Log.Error(ex);
            }

        }
        private void ValidarNomeJaExistente(long? id, string nome)
        {
            if (!nome.LengthValid(false, 2, 50))
                throw new EmpresaInvalidoException("O Nome deve ter entre 2 e 30 caracteres.");

            EmpresaModel Empresa = _report.FindNome(nome);
            if (Empresa.IsNotNull() && Empresa.Id != id)
                throw new EmpresaInvalidoException("Existe outra empresa utilizando este nome.");
        }
        public void Delete(long id)
        {
            try
            {
                EmpresaModel empresa = _repo.Find(id);
                if (empresa.IsNull())
                    throw new EmpresaInvalidoException($"Empresa {id} não encontrado.");

                empresa.Deletar();
                _repo.Save(empresa);
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
        public void Recover(long id)
        {
            try
            {
                EmpresaModel empresa = _repo.Find(id);
                if (empresa.IsNull())
                    throw new EmpresaInvalidoException($"Empresa {id} não encontrado.");

                empresa.Recuperar();
                _repo.Save(empresa);
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
        public EmpresaModel Find(long id)
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
        public EmpresaModel FindCNPJ(string cnpj)
        {
            try
            {
                return _repo.Find(cnpj);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public IEnumerable<EmpresaModel> List()
        {
            try
            {
                return _report.List()
                    .OrderBy(x => x.Nome)
                    .ThenBy(x => x.Deletado);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public void PopularBancoTeste(long idEmpresa)
        {

            EmpresaModel empresa = _repo.Find(idEmpresa);
            _repo.PopularBancoTeste(empresa);

        }
    }
}
