using GoltaraSolutions.SpaWeb.Domain.ClienteContext;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.ClienteContext
{
    public class ClienteRepository : SimpleCRUD<ClienteModel>, IClienteRepository
    {
        public ClienteRepository() : base()
        {
        }
    }
}
