using GoltaraSolutions.Common.Identity.IoC;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.CompositionRoot.Modules;
using Ninject;
using System;

namespace GoltaraSolutions.SpaWeb.CompositionRoot
{
    public class CompositeRootWeb
    {
        public static void SetUp(IKernel container)
        {
            //BootstrapperSection sectio = BootstrapperSection.GetSection();

            //if (sectio.IsNull())
            //    throw new Exception("BootstrapperSection não encontrada no web/app config.");

            //SetUp(container,
            //    sectio.logger,
            //    sectio.repository);
            SetUp(container, LoggerConfig.Log4Net, RepositoryConfig.InMemory);
        }
        private static void SetUp(
            IKernel kernel,
            LoggerConfig loggerStrategy,
            RepositoryConfig repositoryStrategy)
        {
            // Logger
            if (loggerStrategy == LoggerConfig.Log4Net)
            {
                LogLog4NetModule.Build(kernel);
            }
            else
            {
                throw new NotImplementedException();
            }

            Logger.Log = kernel.Get<ILogger>();
            Logger.Log.Inicialize("SpaWeb");
            Logger.Log.Debug("Injeção iniciada.");

            // Repository
            switch (repositoryStrategy)
            {
                case RepositoryConfig.EFSqlServer:
                    RepositoryEFSqlServerModule.Build(kernel);
                    break;

                case RepositoryConfig.InMemory:
                    RepositoryInMemoryModule.Build(kernel);
                    break;

                default:
                    throw new NotImplementedException();
            }


            // Identity
            BootstrapperInjection.Load(kernel);


            Logger.Log.Debug("Injeção concluída com sucesso!");
        }
    }
}
