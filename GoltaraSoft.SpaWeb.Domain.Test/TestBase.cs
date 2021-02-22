using GoltaraSolutions.SpaWeb.CompositionRoot;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Dependency;
using GoltaraSolutions.Common.Infra.Dependency.Ninject;
using GoltaraSolutions.SpaWeb.Infra.Repository.InMemory;

namespace GoltaraSolutions.SpaWeb.Domain.Test
{

    public class TestBase
    {
        protected IDependency _container { get; private set; }

        protected EmpresaModel _empresa = new EmpresaModel("25912083000116", "Goltara Solutions Testes", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");

        protected IEmpresaRepository repoEmpresa;
        protected IEmpresaReport reportEmpresa;

        protected IAtendimentoRepository repoAtendimento;
        protected IAtendimentoReport reportAtendimento;

        protected IClienteRepository repoCliente;
        protected IClienteReport reportCliente;

        protected IFuncionarioRepository repoFuncionario;
        protected IFuncionarioReport reportFuncionario;

        protected IServicoRepository repoServico;
        protected IServicoReport reportServico;

        protected IOrigemRepository repoOrigem;
        protected IOrigemReport reportOrigem;

        protected IProdutoRepository repoProduto;
        protected IProdutoReport reportProduto;

        protected EmpresaDomainServices empresaDomainServices;
        protected ServicoDomainServices servicoDomainServices;
        protected OrigemDomainServices origemDomainServices;
        protected FuncionarioDomainServices funcionarioDomainServices;
        protected ClienteDomainServices clienteDomainServices;
        protected AtendimentoDomainServices atendimentoDomainServices;
        protected ProdutoDomainServices produtoDomainServices;

        public TestBase()
        {
            _container = new NinjectDependency();

            CompositeRoot.SetUp(_container, LoggerConfig.Log4Net, RepositoryConfig.InMemory);

            repoEmpresa = _container.Get<IEmpresaRepository>();
            reportEmpresa = _container.Get<IEmpresaReport>();

            repoAtendimento = _container.Get<IAtendimentoRepository>();
            reportAtendimento = _container.Get<IAtendimentoReport>();

            repoCliente = _container.Get<IClienteRepository>();
            reportCliente = _container.Get<IClienteReport>();

            repoFuncionario = _container.Get<IFuncionarioRepository>();
            reportFuncionario = _container.Get<IFuncionarioReport>();

            repoServico = _container.Get<IServicoRepository>();
            reportServico = _container.Get<IServicoReport>();

            repoOrigem = _container.Get<IOrigemRepository>();
            reportOrigem = _container.Get<IOrigemReport>();

            repoProduto = _container.Get<IProdutoRepository>();
            reportProduto = _container.Get<IProdutoReport>();

            InicializarInMemory();
        }

        protected void Inicializar()
        {

            empresaDomainServices = new EmpresaDomainServices(repoEmpresa, reportEmpresa,
                repoOrigem, reportOrigem,
                repoServico, reportServico,
                repoProduto, reportProduto,
                repoCliente, reportCliente,
                repoFuncionario, reportFuncionario);

            servicoDomainServices = new ServicoDomainServices(repoServico,
                reportServico,
                empresaDomainServices);

            produtoDomainServices = new ProdutoDomainServices(repoProduto,
                reportProduto,
                empresaDomainServices);

            origemDomainServices = new OrigemDomainServices(repoOrigem,
                reportOrigem,
                empresaDomainServices);

            funcionarioDomainServices = new FuncionarioDomainServices(repoFuncionario,
                reportFuncionario,
                servicoDomainServices,
                empresaDomainServices);

            clienteDomainServices = new ClienteDomainServices(repoCliente,
                reportCliente,
                origemDomainServices,
                empresaDomainServices);

            atendimentoDomainServices = new AtendimentoDomainServices(repoAtendimento,
                reportAtendimento,
                empresaDomainServices,
                clienteDomainServices,
                funcionarioDomainServices,
                servicoDomainServices);
        }

        protected void InicializarInMemory()
        {
            var _repoEmpresa = new EmpresaRepository();
            repoEmpresa = _repoEmpresa;
            reportEmpresa = _repoEmpresa;

            var _repoAtendimento = new AtendimentoRepository();
            repoAtendimento = _repoAtendimento;
            reportAtendimento = _repoAtendimento;

            var _repoCliente = new ClienteRepository();
            repoCliente = _repoCliente;
            reportCliente = _repoCliente;

            var _repoFuncionario = new FuncionarioRepository();
            repoFuncionario = _repoFuncionario;
            reportFuncionario = _repoFuncionario;

            var _repoServico = new ServicoRepository();
            repoServico = _repoServico;
            reportServico = _repoServico;

            var _repoOrigem = new OrigemRepository();
            repoOrigem = _repoOrigem;
            reportOrigem = _repoOrigem;

            var _repoProduto = new ProdutoRepository();
            repoProduto = _repoProduto;
            reportProduto = _repoProduto;

            empresaDomainServices = new EmpresaDomainServices(_repoEmpresa, _repoEmpresa,
                _repoOrigem, _repoOrigem,
                _repoServico, _repoServico,
                _repoProduto, _repoProduto,
                _repoCliente, _repoCliente,
                _repoFuncionario, _repoFuncionario);

            servicoDomainServices = new ServicoDomainServices(_repoServico,
                _repoServico,
                empresaDomainServices);

            produtoDomainServices = new ProdutoDomainServices(_repoProduto,
                _repoProduto,
                empresaDomainServices);

            origemDomainServices = new OrigemDomainServices(_repoOrigem,
                _repoOrigem,
                empresaDomainServices);

            funcionarioDomainServices = new FuncionarioDomainServices(_repoFuncionario,
                _repoFuncionario,
                servicoDomainServices,
                empresaDomainServices);

            clienteDomainServices = new ClienteDomainServices(_repoCliente,
                _repoCliente,
                origemDomainServices,
                empresaDomainServices);

            atendimentoDomainServices = new AtendimentoDomainServices(_repoAtendimento,
                _repoAtendimento,
                empresaDomainServices,
                clienteDomainServices,
                funcionarioDomainServices,
                servicoDomainServices);
        }

        protected void CadastrarEmpresa()
        {
            EmpresaModel empresa = repoEmpresa.Find(_empresa.CNPJ);

            if (empresa.IsNull())
                repoEmpresa.Save(_empresa);

            _empresa = repoEmpresa.Find(_empresa.CNPJ);
        }

        protected void DeleteEmpresa()
        {
            EmpresaModel empresa = repoEmpresa.FindIncludingAll(_empresa.Id);

            var atendimentos = reportAtendimento.Relatorio(empresa.Id, null, null, null, null, null, null, null, null);
            if (atendimentos != null)
                atendimentos.ForEach(a => repoAtendimento.Delete(a.Id));

            var clientes = reportCliente.List(empresa.Id);
            if (clientes != null)
                clientes.ToList().ForEach(a => repoCliente.Delete(a.Id));

            var origens = reportOrigem.List(empresa.Id);
            if (origens != null)
                origens.ToList().ForEach(a => repoOrigem.Delete(a.Id));

            var funcionarios = reportFuncionario.List(empresa.Id);
            if (funcionarios != null)
                funcionarios.ToList().ForEach(a => repoFuncionario.Delete(a.Id));

            var servicos = reportServico.List(empresa.Id);
            if (servicos != null)
                servicos.ToList().ForEach(a => repoServico.Delete(a.Id));

            var produtos = reportProduto.List(empresa.Id);
            if (produtos != null)
                produtos.ToList().ForEach(a => repoProduto.Delete(a.Id));

            if (empresa.IsNotNull())
                repoEmpresa.Delete(_empresa.Id);
        }
    }
}
