using Dapper;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ClienteContext.Origem
{
    public class OrigemReport : DapperBase, IOrigemReport
    {
        private const string _campos = @" [Id], [Nome], [IdEmpresa], [Deletado] ";

        public ICollection<OrigemModel> List(long idEmpresa)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Cliente.Origem 
                     Where IdEmpresa = @idEmpresa
                        Order by Nome";

            List<OrigemModel> items = new List<OrigemModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<OrigemModel>(consultaSQL, new { idEmpresa });

                foreach (OrigemModel item in result)
                    items.Add(item);
            }

            return items;

        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            string consultaSQL = @"
            SELECT Id, Nome
                FROM Cliente.Origem 
                    Where IdEmpresa = @idEmpresa AND (@deletado is null Or Deletado = @deletado)
                        Order by Nome";

            List<FiltrosReportView> items = new List<FiltrosReportView>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<FiltrosReportView>(consultaSQL,
                    new { idEmpresa, deletado });

                foreach (FiltrosReportView item in result)
                    items.Add(item);
            }

            return items;
        }

        public OrigemModel Find(long idEmpresa, string nome)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Cliente.Origem 
                Where IdEmpresa = @idEmpresa AND (@nome is null Or Nome like @nome)
                    Order by Nome";

            nome = $"%{nome}%";

            List<OrigemModel> items = new List<OrigemModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<OrigemModel>(consultaSQL,
                    new { idEmpresa, nome });

                foreach (OrigemModel item in result)
                    items.Add(item);
            }

            if (items == null || items.Count == 0)
                return null;

            return items.First();
        }
    }
}
