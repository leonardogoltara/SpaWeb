using System.ComponentModel.DataAnnotations;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public sealed class OrigemViewModel : AggregateViewModel
    {
        public OrigemViewModel()
        {
        }
        
        [MaxLength(15, ErrorMessage = "O {0} deve ter no maximo {1} letras."),
          MinLength(3, ErrorMessage = "O {0} deve ter pelo menos {1} letras.")]
        public string Nome { get; set; }

    }
}
