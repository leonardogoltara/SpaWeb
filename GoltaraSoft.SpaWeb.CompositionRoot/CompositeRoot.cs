using GoltaraSolutions.Common.Infra.Dependency;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.CompositionRoot.Modules;
using System;

namespace GoltaraSolutions.SpaWeb.CompositionRoot
{
    public class CompositeRoot
    {
        public static void SetUp(IDependency container)
        {
            //BootstrapperSection sectio = BootstrapperSection.GetSection();

            //if (sectio.IsNull())
            //    throw new Exception("BootstrapperSection não encontrada no web/app config.");

            //SetUp(container,
            //    sectio.logger,
            //    sectio.repository);
            SetUp(container, LoggerConfig.Log4Net, RepositoryConfig.InMemory);
        }

        public static void SetUp(
            IDependency container,
            LoggerConfig loggerStrategy,
            RepositoryConfig repositoryStrategy)
        {
            // Logger
            if (loggerStrategy == LoggerConfig.Log4Net)
            {
                LogLog4NetModule.Build(container);
            }
            else
            {
                throw new NotImplementedException();
            }

            Logger.Log = container.Get<ILogger>();
            Logger.Log.Inicialize("SpaWeb");
            Logger.Log.Debug("Injeção iniciada.");

            // Repository
            switch (repositoryStrategy)
            {
                case RepositoryConfig.EFSqlServer:
                    RepositoryEFSqlServerModule.Build(container);
                    break;

                case RepositoryConfig.InMemory:
                    RepositoryInMemoryModule.Build(container);
                    break;
                case RepositoryConfig.DapperSqlServer:
                    RepositoryDapperSqlServerModule.Build(container);
                    break;

                default:
                    throw new NotImplementedException();
            }

            Logger.Log.Debug("Injeção concluída com sucesso!");
        }
    }
}
