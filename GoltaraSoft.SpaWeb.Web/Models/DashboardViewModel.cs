using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class DashboardViewModel
    {
        public long AtendimentosTodos { get; set; }
        public long AtendimentosAbertos { get; set; }
        public long AtendimentosCancelados { get; set; }
        public long AtendimentosConcluidos { get; set; }
        public List<TopFuncionario> Top10Funcionarios { get; set; }
        public List<TopCliente> Top10Clientes { get; set; }
        public List<AniversarianteReportView> ClientesAniversariantesMes { get; set; }
    }
}