using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class ServicoRepository : GenericRepository<ServicoModel>, IServicoRepository, IServicoReport
    {
        public ServicoRepository()
        {
            EmpresaModel empresa = new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");

            Add(new ServicoModel(empresa, "Corte", 25, true));
            Add(new ServicoModel(empresa, "Luzes", 60, true));
            Add(new ServicoModel(empresa, "Relaxamento", 90, false));
            Add(new ServicoModel(empresa, "Hidratação", 80, false));
            Add(new ServicoModel(empresa, "Progressiva", 80, false));
            Add(new ServicoModel(empresa, "Sombrancelha", 30, false));
            Add(new ServicoModel(empresa, "Sombrancelha Definitiva", 30, false));
            Add(new ServicoModel(empresa, "Unhas - Pés", 20, false));
            Add(new ServicoModel(empresa, "Unhas - Mãos", 20, false));
            Add(new ServicoModel(empresa, "Unhas - Pés/Mãos", 30, false));
        }

        public override ServicoModel Find(long idEmpresa, string nome)
        {
            return this.SingleOrDefault(o => o.Nome == nome && o.IdEmpresa == idEmpresa);
        }

        public IEnumerable<FiltrosReportView> FindByFuncionario(long idEmpresa, long idFuncionario)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();

            if (this.Count() == 0)
                return null;

            foreach (ServicoModel item in this.List(idEmpresa)?.Where(s => s.IdEmpresa == idEmpresa && s.Funcionarios.Select(f => f.Id).Contains(idFuncionario))?.ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();
            foreach (ServicoModel item in this.List(idEmpresa).Where(s => (deletado == null || s.Deletado == deletado)).ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }
    }
}