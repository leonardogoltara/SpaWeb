using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class ClienteRepository
        : GenericRepository<ClienteModel>, IClienteRepository, IClienteReport
    {
        public ClienteRepository()
        {
            EmpresaModel empresa = new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");

            OrigemModel o1 = new OrigemModel(empresa, "Facebook");
            OrigemModel o2 = new OrigemModel(empresa, "Site");
            OrigemModel o3 = new OrigemModel(empresa, "Indicação de Cliente");

            Add(new ClienteModel(empresa, "Leo Cliente 5", new DateTime(1994, 5, 7), "(11) 4555-1463", "(13) 97164-5267", "leo5@gmail.com", "M", o1));
            Add(new ClienteModel(empresa, "Leo Cliente 4", new DateTime(1994, 5, 7), "(11) 4555-1463", "(13) 97164-5267", "leo4@gmail.com", "M", o2));
            Add(new ClienteModel(empresa, "Leo Cliente 3", new DateTime(1994, 5, 7), "(11) 4555-1463", "(13) 97164-5267", "leo3@gmail.com", "F", o3));
            Add(new ClienteModel(empresa, "Leo Cliente 2", new DateTime(1994, 5, 7), "(11) 4555-1463", "(13) 97164-5267", "leo2@gmail.com", "M", o1));
            Add(new ClienteModel(empresa, "Leo Cliente 1", new DateTime(1994, 5, 7), "(11) 4555-1463", "(13) 97164-5267", "leo1@gmail.com", "F", o2));
        }
        public override ClienteModel Find(long idEmpresa, string nome)
        {
            return this.SingleOrDefault(o => o.Nome == nome && o.IdEmpresa == idEmpresa);
        }

        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();
            foreach (ClienteModel item in this.List(idEmpresa).Where(s => (deletado == null || s.Deletado == deletado)).ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }

        public ICollection<AniversarianteReportView> AniversariantesMes(long idEmpresa)
        {
            List<AniversarianteReportView> itens = new List<AniversarianteReportView>();

            this.Where(x => x.IdEmpresa == idEmpresa && (x.DataNascimento != null && x.DataNascimento.Value.Month == DateTime.Now.Month)).ToList()
                .ForEach(a => itens.Add(new AniversarianteReportView(a.Nome, a.DataNascimento.Value.ToString("dd/MM/yyyy"))));

            return itens;
        }
    }
}