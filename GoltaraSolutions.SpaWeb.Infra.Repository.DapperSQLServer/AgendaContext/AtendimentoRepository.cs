using System;
using System.Linq;
using Dapper;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.AgendaContext
{
    public class AtendimentoRepository : SimpleCRUD<AtendimentoModel>, IAtendimentoRepository
    {
        public AtendimentoRepository() : base()
        {
        }

        public AtendimentoModel GetAtendimento(long idCliente, long idServico, long idFuncionario, DateTime DataHora)
        {
            using (var sqlConnection = Connection())
            {
                var query = sqlConnection.Query<AtendimentoModel>(@"
SELECT * FROM Agenda.Atendimento a
	INNER JOIN Cliente.Cliente c
		ON a.IdCliente = c.Id
	INNER JOIN Servico.Servico s
		ON a.IdServico = s.Id
	INNER JOIN Funcionario.Funcionario f
		ON a.IdFuncionario = f.Id
WHERE (1=1)
    AND (@idCliente IS NULL OR a.idCliente = @idCliente)
    AND (@idServico IS NULL OR a.idServico = @idServico)
    AND (@idFuncionario IS NULL OR a.idFuncionario = @idFuncionario)
    AND (@DataHora IS NULL OR a.DataHora = @DataHora)
", new { idCliente, idServico, idFuncionario, DataHora }, null, false, _commandTimeout);

                if (query != null)
                {
                    return query?.First();
                }
            }

            return null;
        }
    }
}
