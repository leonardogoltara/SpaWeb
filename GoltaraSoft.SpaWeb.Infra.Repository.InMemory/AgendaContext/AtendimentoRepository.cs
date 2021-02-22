using System;
using System.Linq;
using System.Collections.Generic;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class AtendimentoRepository : GenericRepository<AtendimentoModel>, IAtendimentoRepository, IAtendimentoReport
    {
        public AtendimentoRepository()
        {
        }

        public AtendimentoModel GetAtendimento(long idCliente, long idServico, long idFuncionario, DateTime DataHora)
        {
            return this.AsEnumerable().Where(x => x.IdCliente == idCliente
                && x.IdServico == idServico
                && x.IdFuncionario == idFuncionario
                && x.DataHora == DataHora).FirstOrDefault();
        }

        public AtendimentoModel GetByPeriodo(long idEmpresa, DateTime DataInicial, DateTime DataFinal)
        {
            return this.AsEnumerable().Where(x => x.DataHora >= DataInicial && x.DataHora <= DataFinal).FirstOrDefault();
        }

        public List<TopCliente> Top10Clientes(long idEmpresa, DateTime DataInicial, DateTime DataFinal)
        {
            List<TopCliente> report = this
               .Where(a => a.Cancelado == false
                   && a.Concluido == true
                   && a.DataHora > DataInicial
                   && a.DataHora < DataFinal)
               .GroupBy(c => c.Cliente.Nome)
               .Select(g => new TopCliente()
               { ClienteNome = g.Key, Valor = g.Sum(ri => ri.Valor) })
               .OrderByDescending(x => x.Valor)
               .Take(10)
               .AsEnumerable()
               .Select((r, i) => new TopCliente()
               {
                   Posicao = (i + 1).ToString() + "º",
                   ClienteNome = r.ClienteNome,
                   Valor = r.Valor
               })
               .ToList();

            return report;
        }

        public List<TopFuncionario> RankingFuncionarios(long idEmpresa, DateTime DataInicial, DateTime DataFinal)
        {
            List<TopFuncionario> report = this
                .Where(a => a.Cancelado == false
                    && a.Concluido == true
                    && a.DataHora > DataInicial
                    && a.DataHora < DataFinal)
                .GroupBy(c => c.Funcionario.Nome)
                .Select(g => new TopFuncionario()
                { FuncionarioNome = g.Key, Valor = g.Sum(ri => ri.Valor) })
                .OrderByDescending(x => x.Valor)
                .AsEnumerable()
                .Select((r, i) => new TopFuncionario()
                {
                    Posicao = (i + 1).ToString() + "º",
                    FuncionarioNome = r.FuncionarioNome,
                    Valor = r.Valor
                })
                .ToList();

            return report;
        }

        /// <summary>
        /// Não deve ser usado.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public override AtendimentoModel Find(long idEmpresa, string nome)
        {
            throw new NotImplementedException("Este modelo não tem esta função.");
        }

        List<AtendimentoReportView> IAtendimentoReport.Relatorio(long idEmpresa, DateTime? DataInicial, DateTime? DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return new List<AtendimentoReportView>();
        }

        public List<AtendimentoReportAgrupadoView> RelatorioPorCliente(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return new List<AtendimentoReportAgrupadoView>();
        }

        public List<AtendimentoReportAgrupadoView> RelatorioPorServico(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return new List<AtendimentoReportAgrupadoView>();
        }

        public List<AtendimentoReportAgrupadoView> RelatorioPorFuncionario(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return new List<AtendimentoReportAgrupadoView>();
        }

        public List<AtendimentoReportAgrupadoView> RelatorioPorOrigem(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            return new List<AtendimentoReportAgrupadoView>();
        }

        long IAtendimentoReport.Indicador(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, bool? Cancelado, bool? Concluido)
        {
            return 0;
        }
    }
}
