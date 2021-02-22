using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.ServicoContext
{
    public class ServicoRepository : SimpleCRUD<ServicoModel>, IServicoRepository
    {
        public ServicoRepository() : base()
        {
            //try
            //{
            //    FluentMapper.Initialize(config =>
            //    {
            //        config.AddMap(new ServicoMap());
            //        config.ForDommel();
            //    });
            //}
            //catch (System.InvalidOperationException)
            //{
            //}
            //catch (System.Exception)
            //{
            //    throw;
            //}
        }
    }
}
