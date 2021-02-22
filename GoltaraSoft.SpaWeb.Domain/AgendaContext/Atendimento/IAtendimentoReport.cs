using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using System;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento
{
    public interface IAtendimentoReport
    {
        List<AtendimentoReportView> Relatorio(long idEmpresa, DateTime? DataInicial, DateTime? DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido);
        List<AtendimentoReportAgrupadoView> RelatorioPorCliente(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido);
        List<AtendimentoReportAgrupadoView> RelatorioPorServico(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido);
        List<AtendimentoReportAgrupadoView> RelatorioPorFuncionario(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido);
        List<AtendimentoReportAgrupadoView> RelatorioPorOrigem(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido);
  
        /// <summary>
        /// Retorna a quantidade de atendimentos de acordo com os filtros.
        /// </summary>
        /// <param name="DataInicial"></param>
        /// <param name="DataFinal"></param>
        /// <param name="IDCliente"></param>
        /// <param name="IDServico"></param>
        /// <param name="IDFuncionario"></param>
        /// <param name="Cancelado"></param>
        /// <param name="Concluido"></param>
        /// <returns></returns>
        long Indicador(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico, long? idFuncionario, bool? Cancelado, bool? Concluido);
        List<TopCliente> Top10Clientes(long idEmpresa, DateTime DataInicial, DateTime DataFinal);
        List<TopFuncionario> RankingFuncionarios(long idEmpresa, DateTime DataInicial, DateTime DataFinal);
    }
}
