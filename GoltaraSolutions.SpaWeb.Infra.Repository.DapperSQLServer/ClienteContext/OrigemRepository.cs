using GoltaraSolutions.SpaWeb.Domain.ClienteContext;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.ClienteContext
{
    public class OrigemRepository : SimpleCRUD<OrigemModel>, IOrigemRepository
    {
        public OrigemRepository() : base()
        {
        }
    }
}
