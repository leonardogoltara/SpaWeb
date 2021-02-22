using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class ProdutoRepository
        : GenericRepository<ProdutoModel>, IProdutoRepository, IProdutoReport
    {
        public ProdutoRepository()
        {
            EmpresaModel empresa = new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");

            Add(new ProdutoModel(empresa, "Gel", 10));
            Add(new ProdutoModel(empresa, "Creme Hidratante", 45));
        }
        public override ProdutoModel Find(long idEmpresa, string nome)
        {
            return this.SingleOrDefault(o => o.Nome == nome && o.IdEmpresa == idEmpresa);
        }
        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();
            foreach (ProdutoModel item in this.List(idEmpresa).Where(s => (deletado == null || s.Deletado == deletado)).ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }
    }
}
