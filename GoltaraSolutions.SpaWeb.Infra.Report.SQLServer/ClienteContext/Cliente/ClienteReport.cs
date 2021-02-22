using Dapper;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using System.Collections.Generic;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using System.Linq;
using GoltaraSolutions.Common.Domain.Report;
using System.Data;
using System;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ClienteContext.Cliente
{
    public class ClienteReport : DapperBase, IClienteReport
    {
        private const string _campos = @" [Id],[IdOrigem],[Nome],[Sexo],[DataNascimento],[Telefone],[Celular],[Email],[IdEmpresa],[Deletado] ";

        public ICollection<AniversarianteReportView> AniversariantesMes(long idEmpresa)
        {
            int Mes = System.DateTime.Now.Month;
            string consultaSQL = @"
            SELECT Nome, FORMAT(DataNascimento, 'dd/MM') as DataNascimento FROM Cliente.Cliente 
                Where IdEmpresa = @idEmpresa AND Deletado = 0 AND Month(DataNascimento) = @Mes
                    Order by Nome";

            List<AniversarianteReportView> items = new List<AniversarianteReportView>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<AniversarianteReportView>(consultaSQL,
                    new { idEmpresa, Mes });

                foreach (AniversarianteReportView item in result)
                    items.Add(item);
            }

            return items;
        }

        public ICollection<ClienteModel> List(long idEmpresa)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Cliente.Cliente 
                Where IdEmpresa = @idEmpresa
                    Order by Nome";

            List<ClienteModel> items = new List<ClienteModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<ClienteModel>(consultaSQL,
                    new { idEmpresa });

                foreach (ClienteModel item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            string consultaSQL = @"
            SELECT Id, Nome FROM Cliente.Cliente 
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

        public ClienteModel Find(long idEmpresa, string nome)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Cliente.Cliente 
                Where IdEmpresa = @idEmpresa AND (@nome is null Or Nome like @nome)
                    Order by Nome";

            nome = $"%{nome}%";

            List<ClienteModel> items = new List<ClienteModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<ClienteModel>(consultaSQL,
                    new { idEmpresa, nome });

                foreach (ClienteModel item in result)
                    items.Add(item);
            }

            if (items == null || items.Count == 0)
                return null;

            return items.First();
        }
    }
}
