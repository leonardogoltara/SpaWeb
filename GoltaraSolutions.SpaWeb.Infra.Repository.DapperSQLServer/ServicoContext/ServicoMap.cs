using Dapper.FluentMap.Dommel.Mapping;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.ServicoContext
{
    public class ServicoMap : DommelEntityMap<ServicoModel>
    {
        public ServicoMap()
        {
            ToTable("Servico", "Servico");
        }
    }
}
