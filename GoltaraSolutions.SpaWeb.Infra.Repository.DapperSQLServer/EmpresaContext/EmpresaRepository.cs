using Dapper;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.EmpresaContext
{
    public class EmpresaRepository : SimpleCRUD<EmpresaModel>, IEmpresaRepository
    {
        public EmpresaRepository() : base()
        {
        }

        public override void Save(EmpresaModel model)
        {
            base.Save(model);

            using (var sqlConnection = Connection())
            {
                if (sqlConnection.Get<EmpresaResponsavelCobranca>(model.ResponsavelCobranca.IdEmpresa, null, _commandTimeout) == null)
                {
                    sqlConnection.Insert(model.ResponsavelCobranca, null, _commandTimeout);
                }
                else
                {
                    sqlConnection.Update(model.ResponsavelCobranca, null, _commandTimeout);
                }
            }
        }

        public EmpresaModel Find(string cnpj)
        {
            using (var sqlConnection = Connection())
            {
                var query = sqlConnection.Query<EmpresaModel>(@"
SELECT * 
    FROM Empresa.Empresa e
        LEFT JOIN Empresa.ResponsavelCobranca rc
            ON e.Id = rc.IdEmpresa
		LEFT JOIN Servico.Servico s
			ON e.Id = s.IdEmpresa
    WHERE e.CNPJ like @cnpj 
", new { cnpj }, null, false, _commandTimeout);

                if (query != null)
                {
                    if (query != null && query.ToList().Count > 0)
                        return query.FirstOrDefault();
                }
            }

            return null;
        }

        public EmpresaModel FindIncludingAll(long id)
        {
            using (var sqlConnection = Connection())
            {
                var query = sqlConnection.Query<EmpresaModel>(@"
SELECT * 
    FROM Empresa.Empresa e
        LEFT JOIN Empresa.ResponsavelCobranca rc
            ON e.Id = rc.IdEmpresa
    WHERE e.Id = @id
", new { id }, null, false, _commandTimeout);

                if (query != null)
                {
                    if (query != null && query.ToList().Count > 0)
                        return query.FirstOrDefault();
                }
            }

            return null;
        }

        public void PopularBancoTeste(EmpresaModel empresa)
        {
            throw new System.NotImplementedException();
        }
    }
}
