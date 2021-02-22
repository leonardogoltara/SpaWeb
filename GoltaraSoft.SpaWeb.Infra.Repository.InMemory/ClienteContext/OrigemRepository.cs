using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class OrigemRepository : GenericRepository<OrigemModel>, IOrigemRepository, IOrigemReport
    {
        public OrigemRepository()
        {
            EmpresaModel empresa = new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");

            Add(new OrigemModel(empresa, "Facebook"));
            Add(new OrigemModel(empresa, "Site"));
            Add(new OrigemModel(empresa, "Panfleto"));
            Add(new OrigemModel(empresa, "Indicação de Funcionário"));
            Add(new OrigemModel(empresa, "Indicação de Cliente"));
            Add(new OrigemModel(empresa, "Blog"));
            Add(new OrigemModel(empresa, "Instagram"));
            Add(new OrigemModel(empresa, "Display de Propaganda"));
            Add(new OrigemModel(empresa, "Outdoor"));
            Add(new OrigemModel(empresa, "Twitter"));
            Add(new OrigemModel(empresa, "Feiras de Exposição"));
            Add(new OrigemModel(empresa, "Outros"));
        }
        
        public override OrigemModel Find(long idEmpresa, string nome)
        {
            return this.SingleOrDefault(o => o.Nome == nome && o.IdEmpresa == idEmpresa);
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();
            foreach (OrigemModel item in this.List(idEmpresa).Where(s => (deletado == null || s.Deletado == deletado)).ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }
    }
}
