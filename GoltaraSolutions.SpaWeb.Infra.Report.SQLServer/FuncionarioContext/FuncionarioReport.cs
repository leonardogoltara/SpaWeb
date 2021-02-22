using Dapper;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.FuncionarioContext
{
    public class FuncionarioReport : DapperBase, IFuncionarioReport
    {
        private const string _campos = @" [Id], [Nome], [Telefone], [Celular], [Email], [Sexo], [IdEmpresa], [Deletado] ";

        public ICollection<FuncionarioModel> List(long idEmpresa)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Funcionario.Funcionario 
                     Where IdEmpresa = @idEmpresa
                    Order by Nome";

            List<FuncionarioModel> items = new List<FuncionarioModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<FuncionarioModel>(consultaSQL, new { idEmpresa });

                foreach (FuncionarioModel item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            string consultaSQL = @"
            SELECT Id, Nome
                FROM Funcionario.Funcionario 
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

        public FuncionarioModel Find(long idEmpresa, string nome)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Funcionario.Funcionario 
                Where IdEmpresa = @idEmpresa AND (@nome is null Or Nome like @nome)
                    Order by Nome";

            nome = $"%{nome}%";

            List<FuncionarioModel> items = new List<FuncionarioModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<FuncionarioModel>(consultaSQL,
                    new { idEmpresa, nome });

                foreach (FuncionarioModel item in result)
                    items.Add(item);
            }
            
            if (items == null || items.Count == 0)
                return null;

            return items.First();
        }
    }
}
