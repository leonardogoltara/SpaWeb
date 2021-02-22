using System;
using System.Collections.Generic;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using Dapper;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer.AgendaContext.Atendimento
{
    public class AtendimentoReport : DapperBase, IAtendimentoReport
    {
        private const string QueryConsultaCompleta = @"
               Select 
	                a.Id AS 'Id',
	                a.DataHora,
	                c.Nome AS 'Cliente',
	                c.Id AS 'IdCliente',
	                o.Nome AS 'Origem',
	                o.Id AS 'IdOrigem',
	                f.Nome AS 'Funcionario',
	                f.Id AS 'IdFuncionario',
	                s.Nome AS 'Servico',
	                s.Id AS 'IdServico',
                    s.PrecoFixo AS 'PrecoFixo',
	                a.Valor,
	                a.Concluido,
	                a.Cancelado
                From [Agenda].[Atendimento] a
                INNER JOIN [Cliente].[Cliente] c
	                ON a.IdCliente = c.Id
                        AND a.IdEmpresa = c.IdEmpresa
                INNER JOIN [Cliente].[Origem] o
	                ON c.IdOrigem = o.Id
                        AND a.IdEmpresa = o.IdEmpresa
                INNER JOIN [Servico].[Servico] s
	                ON a.IdServico = s.Id
                        AND a.IdEmpresa = s.IdEmpresa
                INNER JOIN [Funcionario].[Funcionario] f
	                ON a.IdFuncionario = f.Id
		                AND a.IdEmpresa = c.IdEmpresa
                Where ((@DataInicial Is Null OR @DataFinal Is Null) 
						OR (a.DataHora BETWEEN @DataInicial AND @DataFinal))
	                AND a.IdEmpresa = @IdEmpresa
	                AND a.Confirmado = 1
	                AND (@idServico is null OR a.IdServico = @idServico)
	                AND (@idOrigem is null OR c.IdOrigem = @idOrigem)
	                AND (@idCliente is null OR a.IdCliente = @idCliente)
	                AND (@idFuncionario is null OR a.IdFuncionario = @idFuncionario)
	                AND (@Cancelado is null OR a.Cancelado = @Cancelado)
	                AND (@Concluido is null OR a.Concluido = @Concluido)";

        public long Indicador(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, bool? Cancelado, bool? Concluido)
        {
            string consultaSQL = @"
                Select 
                	COUNT(1)
                From [Agenda].[Atendimento] a
                INNER JOIN [Cliente].[Cliente] c
                	ON a.IdCliente = c.Id
                INNER JOIN [Servico].[Servico] s
                	ON a.IdServico = s.Id
                INNER JOIN [Funcionario].[Funcionario] f
                	ON a.IdFuncionario = f.Id
                		AND a.IdEmpresa = c.IdEmpresa
                Where ((@DataInicial Is Null OR @DataFinal Is Null) 
						OR (a.DataHora BETWEEN @DataInicial AND @DataFinal))
                	AND a.IdEmpresa = @IdEmpresa
                	AND a.Confirmado = 1
                	AND (@idServico is null OR a.IdServico = @idServico)
                	AND (@idCliente is null OR a.IdCliente = @idCliente)
                	AND (@idFuncionario is null OR a.IdFuncionario = @idFuncionario)
                	AND (@Cancelado is null OR a.Cancelado = @Cancelado)
                	AND (@Concluido is null OR a.Concluido = @Concluido)";

            long indicador = 0;

            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<long>(consultaSQL,
                    new { idEmpresa, DataInicial, DataFinal, idServico, idCliente, idFuncionario, Cancelado, Concluido });

                foreach (long ind in result)
                    indicador = ind;
            }

            return indicador;
        }

        public List<TopFuncionario> RankingFuncionarios(long idEmpresa, DateTime DataInicial, DateTime DataFinal)
        {
            string consultaSQL = @"
                Select 
                	CONVERT(varchar(2), ROW_NUMBER() OVER(ORDER BY SUM(a.Valor) DESC)) + 'º' AS 'Posicao',
                	f.Nome AS 'FuncionarioNome',
                	SUM(a.Valor) AS 'Valor'
                From [Agenda].[Atendimento] a
                INNER JOIN [Funcionario].[Funcionario] f
                	ON a.IdFuncionario = f.Id
                		AND a.IdEmpresa = f.IdEmpresa
                Where Cancelado = 0
                	AND a.Concluido = 1
                	AND ((@DataInicial Is Null OR @DataFinal Is Null) 
						OR (a.DataHora BETWEEN @DataInicial AND @DataFinal))
                	AND a.IdEmpresa = @IdEmpresa
                GROUP BY f.Id, f.Nome
                ORDER BY ROW_NUMBER() OVER(ORDER BY SUM(a.Valor) DESC)";

            List<TopFuncionario> items = new List<TopFuncionario>();

            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<TopFuncionario>(consultaSQL,
                    new { idEmpresa, DataInicial, DataFinal });

                foreach (TopFuncionario item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<AtendimentoReportView> Relatorio(long idEmpresa, DateTime? DataInicial, DateTime? DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            string consultaSQL = QueryConsultaCompleta;

            List<AtendimentoReportView> items = new List<AtendimentoReportView>();

            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<AtendimentoReportView>(consultaSQL,
                    new { idEmpresa, DataInicial, DataFinal, idServico, idCliente, idFuncionario, idOrigem, Cancelado, Concluido });

                foreach (AtendimentoReportView item in result)
                    items.Add(item);
            }

            return items;
        }

        public List<AtendimentoReportAgrupadoView> RelatorioPorCliente(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return RelatorioAgrupado("Cliente", idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
        }
        public List<AtendimentoReportAgrupadoView> RelatorioPorServico(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return RelatorioAgrupado("Servico", idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
        }
        public List<AtendimentoReportAgrupadoView> RelatorioPorFuncionario(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return RelatorioAgrupado("Funcionario", idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
        }
        public List<AtendimentoReportAgrupadoView> RelatorioPorOrigem(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return RelatorioAgrupado("Origem", idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
        }
        private List<AtendimentoReportAgrupadoView> RelatorioAgrupado(string tipo, long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            string consultaSQL = $@"Select {tipo} AS 'Titulo', COUNT(Id) AS 'Quantidade', SUM(Valor) AS 'Valor' 
                FROM ({QueryConsultaCompleta}) AS T 
                GROUP BY Id{tipo}, {tipo}";

            List<AtendimentoReportAgrupadoView> items = new List<AtendimentoReportAgrupadoView>();

            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<AtendimentoReportAgrupadoView>(consultaSQL,
                    new { idEmpresa, DataInicial, DataFinal, idServico, idCliente, idFuncionario, idOrigem, Cancelado, Concluido });

                foreach (AtendimentoReportAgrupadoView item in result)
                    items.Add(item);
            }

            return items;
        }
        public List<TopCliente> Top10Clientes(long idEmpresa, DateTime DataInicial, DateTime DataFinal)
        {
            string consultaSQL = @"
                Select TOP(10)
	                CONVERT(varchar(2), ROW_NUMBER() OVER(ORDER BY SUM(a.Valor) DESC)) + 'º' AS 'Posicao',
	                c.Nome AS 'ClienteNome',
	                SUM(a.Valor) AS 'Valor'
                From [Agenda].[Atendimento] a
                INNER JOIN [Cliente].[Cliente] c
	                ON a.IdCliente = c.Id
		                AND a.IdEmpresa = c.IdEmpresa
                Where Cancelado = 0
	                AND a.Concluido = 1
	                AND ((@DataInicial Is Null OR @DataFinal Is Null) 
						OR (a.DataHora BETWEEN @DataInicial AND @DataFinal))
	                AND a.IdEmpresa = @IdEmpresa
                GROUP BY c.Id, c.Nome
                ORDER BY ROW_NUMBER() OVER(ORDER BY SUM(a.Valor) DESC)";

            List<TopCliente> items = new List<TopCliente>();

            using (var sqlConnection = base.Connection())
            {
                var result = sqlConnection.Query<TopCliente>(consultaSQL,
                    new { idEmpresa, DataInicial, DataFinal });

                foreach (TopCliente item in result)
                    items.Add(item);
            }

            return items;
        }
    }
}
