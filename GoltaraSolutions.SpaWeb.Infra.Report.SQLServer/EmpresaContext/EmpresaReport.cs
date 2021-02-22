using Dapper;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.EmpresaContext
{
    public class EmpresaReport : DapperBase, IEmpresaReport
    {
        private const string _campos = @" [Id], [CNPJ], [Nome], [Deletado] ";

        public ICollection<EmpresaModel> List()
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Empresa.Empresa 
                    Order by Nome";

            List<EmpresaModel> items = new List<EmpresaModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<EmpresaModel>(consultaSQL);

                foreach (EmpresaModel item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<FiltrosReportView> ListarFiltros(bool? deletado)
        {
            string consultaSQL = @"
            SELECT Id, Nome
                FROM Empresa.Empresa 
                    Where (@deletado is null Or Deletado = @deletado)
                        Order by Nome";

            List<FiltrosReportView> items = new List<FiltrosReportView>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<FiltrosReportView>(consultaSQL,
                    new { deletado });

                foreach (FiltrosReportView item in result)
                    items.Add(item);
            }

            return items;
        }

        public EmpresaModel FindNome(string nome)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Empresa.Empresa 
                Where (@nome is null Or Nome like @nome)
                    Order by Nome";

            nome = $"%{nome}%";

            List<EmpresaModel> items = new List<EmpresaModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<EmpresaModel>(consultaSQL,
                    new { nome });

                foreach (EmpresaModel item in result)
                    items.Add(item);
            }

            if (items == null || items.Count == 0)
                return null;

            return items.First();
        }
    }
}
