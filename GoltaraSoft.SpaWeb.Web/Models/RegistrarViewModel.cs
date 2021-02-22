using System.ComponentModel.DataAnnotations;

namespace GoltaraSoft.SysBeauty.Web.Models
{
    public class RegistrarViewModel : GoltaraSolutions.Common.Identity.Models.RegistroViewModel
    {
        [Display(Name = "Nome da Empresa")]
        [MaxLength(30, ErrorMessage = "O {0} deve ter no maximo {1} letras."),
            MinLength(3, ErrorMessage = "O {0} deve ter pelo menos {1} letras.")]
        [Required(ErrorMessage = "O campo {0} é obrigado.")]
        public string EmpresaNome { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O campo {0} é obrigado.")]
        public string EmpresaCNPJ { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O campo {0} é obrigado.")]
        public string Celular { get; set; }


        public bool Aceite { get; set; }
    }
}