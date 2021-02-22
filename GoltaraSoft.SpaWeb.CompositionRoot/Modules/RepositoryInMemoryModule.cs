using GoltaraSolutions.Common.Infra.Dependency;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.InMemory;
using Ninject;

namespace GoltaraSolutions.SpaWeb.CompositionRoot.Modules
{
    public class RepositoryInMemoryModule
    {
        public static void Build(IDependency container)
        {
            container.Bind<IClienteRepository>(container.Get<ClienteRepository>());
            container.Bind<IClienteReport>(container.Get<ClienteRepository>());

            container.Bind<IFuncionarioRepository>(container.Get<FuncionarioRepository>());
            container.Bind<IFuncionarioReport>(container.Get<FuncionarioRepository>());

            container.Bind<IServicoRepository>(container.Get<ServicoRepository>());
            container.Bind<IServicoReport>(container.Get<ServicoRepository>());

            container.Bind<IProdutoRepository>(container.Get<ProdutoRepository>());
            container.Bind<IProdutoReport>(container.Get<ProdutoRepository>());

            container.Bind<IAtendimentoRepository>(container.Get<AtendimentoRepository>());
            container.Bind<IAtendimentoReport>(container.Get<AtendimentoRepository>());

            container.Bind<IOrigemRepository>(container.Get<OrigemRepository>());
            container.Bind<IOrigemReport>(container.Get<OrigemRepository>());

            container.Bind<IEmpresaRepository>(container.Get<EmpresaRepository>());
            container.Bind<IEmpresaReport>(container.Get<EmpresaRepository>());
        }
        public static void Build(IKernel kernel)
        {
            kernel.Bind<IClienteRepository>().To<ClienteRepository>().InSingletonScope();
            kernel.Bind<IClienteReport>().To<ClienteRepository>().InSingletonScope();

            kernel.Bind<IFuncionarioRepository>().To<FuncionarioRepository>().InSingletonScope();
            kernel.Bind<IFuncionarioReport>().To<FuncionarioRepository>().InSingletonScope();

            kernel.Bind<IServicoRepository>().To<ServicoRepository>().InSingletonScope();
            kernel.Bind<IServicoReport>().To<ServicoRepository>().InSingletonScope();

            kernel.Bind<IProdutoRepository>().To<ProdutoRepository>().InSingletonScope();
            kernel.Bind<IProdutoReport>().To<ProdutoRepository>().InSingletonScope();

            kernel.Bind<IAtendimentoRepository>().To<AtendimentoRepository>().InSingletonScope();
            kernel.Bind<IAtendimentoReport>().To<AtendimentoRepository>().InSingletonScope();

            kernel.Bind<IOrigemRepository>().To<OrigemRepository>().InSingletonScope();
            kernel.Bind<IOrigemReport>().To<OrigemRepository>().InSingletonScope();

            kernel.Bind<IEmpresaRepository>().To<EmpresaRepository>().InSingletonScope();
            kernel.Bind<IEmpresaReport>().To<EmpresaRepository>().InSingletonScope();
        }

    }
}
