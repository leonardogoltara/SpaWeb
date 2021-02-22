using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class Sexo
    {
        public string Nome { get; set; }
        public string NomeCurto { get; set; }
    }
    public class PessoaViewModel : AggregateViewModel
    {
        public PessoaViewModel()
        {
            //DataNascimento = new DateTime(1980, 01, 01);

            List<Sexo> _sexos = new List<Sexo>()
            {
                new Sexo() { Nome = "Masculino", NomeCurto = "M" },
                new Sexo() { Nome = "Feminino", NomeCurto = "F" }
            };

            Sexos = new SelectList(_sexos, "NomeCurto", "Nome");
        }

        [MaxLength(50, ErrorMessage = "O {0} deve ter no maximo {1} letras."),
            MinLength(3, ErrorMessage = "O {0} deve ter pelo menos {1} letras.")]
        public string Nome { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Text, ErrorMessage = "A {0} tem que ser uma data.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage ="O campo {0} é obrigatório.")]
        public string Email { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "Sexo")]
        public SelectList Sexos { get; set; }
    }
}
