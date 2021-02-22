using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class ClienteViewModel : PessoaViewModel
    {
        public ClienteViewModel()
        {
            //DataNascimento = new DateTime(1980, 01, 01);
        }
        public long OrigemSelecionada { get; set; }
        public SelectList Origens { get; set; }

        [Display(Name = "Canal")]
        public OrigemModel Origem { get; set; }

        [Display(Name = "Histórico")]
        public IEnumerable<AtendimentoReportView> Historico { get; set; }
    }
}
