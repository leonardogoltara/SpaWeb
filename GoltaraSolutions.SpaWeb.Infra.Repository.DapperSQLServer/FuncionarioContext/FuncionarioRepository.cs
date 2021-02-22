using Dapper;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.FuncionarioContext
{
    public class FuncionarioRepository : SimpleCRUD<FuncionarioModel>, IFuncionarioRepository
    {
        public FuncionarioRepository() : base()
        {
        }

        public FuncionarioModel Find(string cnpj)
        {
            using (var sqlConnection = Connection())
            {
                var query = sqlConnection.Query<FuncionarioModel>(@"
SELECT * 
    FROM Empresa.Empresa e
    WHERE e.CNPJ like @cnpj 
", new { cnpj }, null, false, _commandTimeout);

                if (query != null)
                {
                    return query?.First();
                }
            }

            return null;
        }
    }
}
