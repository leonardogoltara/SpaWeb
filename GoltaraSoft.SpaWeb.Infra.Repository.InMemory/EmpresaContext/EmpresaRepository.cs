using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Linq;
using System.Collections.Generic;
using GoltaraSolutions.Common.Domain.Report;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class EmpresaRepository : GenericRepository<EmpresaModel>, IEmpresaRepository, IEmpresaReport
    {
        public EmpresaRepository()
        {
            Add(new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com"));
            Add(new EmpresaModel("71639572000163", "AbalaSoft", "Leonardo 2", "(11) 94555-1463", "lsgolt94@gmail.com"));
        }

        public ICollection<EmpresaModel> List()
        {
            return this.ToList();
        }

        public EmpresaModel Find(string cnpj)
        {
            return this.SingleOrDefault(o => o.CNPJ == cnpj);
        }

        public EmpresaModel FindIncludingAll(long id)
        {
            return Find(id);
        }

        public List<FiltrosReportView> ListarFiltros(bool? deletado)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();
            foreach (EmpresaModel item in List().Where(s => (deletado == null || s.Deletado == deletado)).ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }

        public EmpresaModel FindNome(string nome)
        {
            return this.SingleOrDefault(o => o.Nome.ToLower().Trim() == nome.ToLower().Trim());
        }

        public void PopularBancoTeste(EmpresaModel empresa)
        {
            throw new System.NotImplementedException();
        }
    }
}