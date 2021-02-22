using GoltaraSolutions.Common.Infra.Dependency;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ClienteContext.Cliente;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ClienteContext.Origem;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.EmpresaContext;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ProdutoContext;
using GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.AgendaContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ClienteContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.EmpresaContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ProdutoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ServicoContext;
using Ninject;

namespace GoltaraSolutions.SpaWeb.CompositionRoot.Modules
{
    public class RepositoryEFSqlServerModule
    {
        public static void Build(IDependency container)
        {
            container.Bind<IClienteRepository>(container.Get<ClienteRepository>());
            container.Bind<IClienteReport>(container.Get<ClienteReport>());

            container.Bind<IOrigemRepository>(container.Get<OrigemRepository>());
            container.Bind<IOrigemReport>(container.Get<OrigemReport>());

            container.Bind<IFuncionarioRepository>(container.Get<FuncionarioRepository>());
            container.Bind<IFuncionarioReport>(container.Get<FuncionarioReport>());

            container.Bind<IServicoRepository>(container.Get<ServicoRepository>());
            container.Bind<IServicoReport>(container.Get<ServicoReport>());

            container.Bind<IProdutoRepository>(container.Get<ProdutoRepository>());
            container.Bind<IProdutoReport>(container.Get<ProdutoReport>());

            container.Bind<IAtendimentoRepository>(container.Get<AtendimentoRepository>());
            container.Bind<IAtendimentoReport>(container.Get<AtendimentoReport>());

            container.Bind<IEmpresaRepository>(container.Get<EmpresaRepository>());
            container.Bind<IEmpresaReport>(container.Get<EmpresaReport>());
        }
        public static void Build(IKernel kernel)
        {
            kernel.Bind<IClienteRepository>().To<ClienteRepository>();
            kernel.Bind<IClienteReport>().To<ClienteReport>();

            kernel.Bind<IOrigemRepository>().To<OrigemRepository>();
            kernel.Bind<IOrigemReport>().To<OrigemReport>();

            kernel.Bind<IFuncionarioRepository>().To<FuncionarioRepository>();
            kernel.Bind<IFuncionarioReport>().To<FuncionarioReport>();

            kernel.Bind<IServicoRepository>().To<ServicoRepository>();
            kernel.Bind<IServicoReport>().To<ServicoReport>();

            kernel.Bind<IProdutoRepository>().To<ProdutoRepository>();
            kernel.Bind<IProdutoReport>().To<ProdutoReport>();

            kernel.Bind<IAtendimentoRepository>().To<AtendimentoRepository>();
            kernel.Bind<IAtendimentoReport>().To<AtendimentoReport>();
            
            kernel.Bind<IEmpresaRepository>().To<EmpresaRepository>();
            kernel.Bind<IEmpresaReport>().To<EmpresaReport>();
        }
    }
}
