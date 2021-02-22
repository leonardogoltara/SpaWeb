using System.ComponentModel;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public abstract class AggregateViewModel
    {
        [DisplayName("Código")]
        public long Id { get; set; }
        [DisplayName("Ativo")]
        public string Ativo { get; set; }
        public bool Deletado { get; set; }
    }
}