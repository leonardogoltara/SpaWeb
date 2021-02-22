using Dapper;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ServicoContext
{
    public class ServicoReport : DapperBase, IServicoReport
    {
        private const string _campos = @" [Id], [Nome], [Preco], [PrecoFixo], [IdEmpresa], [Deletado] ";

        public ICollection<ServicoModel> List(long idEmpresa)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Servico.Servico 
                    Where IdEmpresa = @idEmpresa
                        Order by Nome";

            List<ServicoModel> items = new List<ServicoModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<ServicoModel>(consultaSQL, new { idEmpresa });

                foreach (ServicoModel item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            string consultaSQL = @"
            SELECT Id, Nome
                FROM Servico.Servico 
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

        public ServicoModel Find(long idEmpresa, string nome)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Servico.Servico 
                Where IdEmpresa = @idEmpresa AND (@nome is null Or Nome like @nome)
                    Order by Nome";

            nome = $"%{nome}%";

            List<ServicoModel> items = new List<ServicoModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<ServicoModel>(consultaSQL,
                    new { idEmpresa, nome });

                foreach (ServicoModel item in result)
                    items.Add(item);
            }

            if (items == null || items.Count == 0)
                return null;

            return items.First();
        }

        public IEnumerable<FiltrosReportView> FindByFuncionario(long idEmpresa, long idFuncionario)
        {
            string consultaSQL = $@"
            SELECT s.Id as Id, s.Nome as Nome
                FROM Servico.Servico s
                    INNER JOIN Funcionario.FuncionariosServicos fs
	                    ON s.Id = fs.ServicoId
                Where IdEmpresa = @idEmpresa AND fs.FuncionarioId = @idFuncionario
                    Order by Nome";
            
            List<FiltrosReportView> items = new List<FiltrosReportView>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<FiltrosReportView>(consultaSQL,
                    new { idEmpresa, idFuncionario });

                foreach (FiltrosReportView item in result)
                    items.Add(item);
            }

            if (items == null || items.Count == 0)
                return null;

            return items;
        }
    }
}
