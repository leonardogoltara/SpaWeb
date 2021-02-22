using Dapper;
using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.ProdutoContext
{
    public class ProdutoReport : DapperBase, IProdutoReport
    {
        private const string _campos = @" [Id], [Nome], [Preco], [IdEmpresa], [Deletado] ";

        public ICollection<ProdutoModel> List(long idEmpresa)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Produto.Produto 
                    Where IdEmpresa = @idEmpresa
                        Order by Nome";

            List<ProdutoModel> items = new List<ProdutoModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<ProdutoModel>(consultaSQL, new { idEmpresa });

                foreach (ProdutoModel item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            string consultaSQL = @"
            SELECT Id, Nome
                FROM Produto.Produto 
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

        public ProdutoModel Find(long idEmpresa, string nome)
        {
            string consultaSQL = $@"
            SELECT {_campos}
                FROM Produto.Produto 
                Where IdEmpresa = @idEmpresa AND (@nome is null Or Nome like @nome)
                    Order by Nome";

            nome = $"%{nome}%";

            List<ProdutoModel> items = new List<ProdutoModel>();
            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<ProdutoModel>(consultaSQL,
                    new { idEmpresa, nome });

                foreach (ProdutoModel item in result)
                    items.Add(item);
            }


            if (items == null || items.Count == 0)
                return null;

            return items.First();
        }
    }
}
