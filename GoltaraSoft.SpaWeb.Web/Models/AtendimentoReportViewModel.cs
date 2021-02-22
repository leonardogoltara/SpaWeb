using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class AtendimentoReportViewModel
    {
        public AtendimentoReportViewModel()
        {
            DataHoraInicial = DateTime.Now.AddDays(-7).FirstHourOfDay();
            DataHoraFinal = DateTime.Now.AddDays(7).LastHourOfDay();
            Status = Status.Todos;
        }
        public int? IdCliente { get; set; }
        [Display(Name = "Cliente")]
        public SelectList Clientes { get; set; }
        public int? IdServico { get; set; }
        [Display(Name = "Serviço")]
        public SelectList Servicos { get; set; }
        public int? IdFuncionario { get; set; }
        [Display(Name = "Funcionário")]
        public SelectList Funcionarios { get; set; }
        public int? IdOrigem { get; set; }
        [Display(Name = "Canal")]
        public SelectList Origens { get; set; }
        public Status Status { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Text, ErrorMessage = "A {0} tem que ser uma data.")]
        [Required(ErrorMessage = "A {0} é obrigatória.")]
        [Display(Name = "Data Inicial")]
        public DateTime DataHoraInicial { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Text, ErrorMessage = "A {0} tem que ser uma data.")]
        [Required(ErrorMessage = "A {0} é obrigatória.")]
        [Display(Name = "Data Final")]
        public DateTime DataHoraFinal { get; set; }
        public bool? Cancelado { get; set; }
        public bool? Concluido { get; set; }

        public virtual IEnumerable<AtendimentoReportView> Atendimentos { get; set; }


    }
}