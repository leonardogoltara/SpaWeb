using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class ProdutoViewModel : AggregateViewModel
    {
        public ProdutoViewModel()
        {
        }

        [MaxLength(15, ErrorMessage = "O {0} deve ter no maximo {1} letras."),
            MinLength(3, ErrorMessage = "O {0} deve ter pelo menos {1} letras.")]
        public string Nome { get; set; }

        [DisplayName("Preço"), DataType(DataType.Currency)]
        public decimal Preco { get; set; }
    }
}
