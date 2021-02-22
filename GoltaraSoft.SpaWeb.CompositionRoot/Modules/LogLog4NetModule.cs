using GoltaraSolutions.Common.Infra.Dependency;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.Common.Infra.Log.Log4Net;
using Ninject;

namespace GoltaraSolutions.SpaWeb.CompositionRoot.Modules
{
    public class LogLog4NetModule
    {
        public static void Build(IDependency container)
        {
            container.Bind<ILogger>(container.Get<Log4NetLogger>());
        }
        public static void Build(IKernel kernel)
        {
            kernel.Bind<ILogger>().To<Log4NetLogger>().InSingletonScope();
        }
    }
}
