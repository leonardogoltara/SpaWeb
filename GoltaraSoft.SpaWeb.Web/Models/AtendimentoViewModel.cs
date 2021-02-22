using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class AtendimentoViewModel
    {
        public AtendimentoViewModel()
        {
            DataHora = DateTime.Now;
        }
        public long Id { get; set; }
        public long IdCliente { get; set; }
        [Display(Name = "Cliente")]
        public SelectList Clientes { get; set; }
        public long IdServico { get; set; }
        [Display(Name = "Serviço")]
        public SelectList Servicos { get; set; }
        public long IdFuncionario { get; set; }
        [Display(Name = "Funcionário")]
        public SelectList Funcionarios { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DataType(DataType.Text, ErrorMessage = "A {0} tem que ser uma data.")]
        [Display(Name = "Data/Hora")]
        public DateTime DataHora { get; set; }
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }
        public bool Cancelado { get; set; }
        public bool Concluido { get; set; }
        [Display(Name = "Cliente")]
        public ClienteModel Cliente { get; set; }
        [Display(Name = "Serviço")]
        public ServicoModel Servico { get; set; }
        [Display(Name = "Funcionário")]
        public FuncionarioModel Funcionario { get; set; }

        [Display(Name = "Usuário Agendou")]
        public string UsuarioAgendou { get;  set; }
    }
}