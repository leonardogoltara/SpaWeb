using System;

namespace GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews
{
    public class AtendimentoReportView
    {
        public long Id { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DataHoraEncerramento { get; set; }

        public string DataHoraString { get; set; }
        public string DataHoraEncerramentoString { get; set; }

        public string Cliente { get; set; }
        public string Funcionario { get; set; }
        public string Servico { get; set; }
        public bool ServicoPrecoFixo { get; set; }
        public decimal Valor { get; set; }
        public bool Concluido { get; set; }
        public bool Cancelado { get; set; }
        public string Status { get; set; }
    }
    public enum Status
    {
        Aberto = 0,
        Cancelado = 2,
        Concluído = 3,
        Todos = 4
    }
}
