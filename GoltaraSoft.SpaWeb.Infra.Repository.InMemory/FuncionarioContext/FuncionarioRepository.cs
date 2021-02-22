using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;
using System.Linq;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.Common.Domain.Report;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public sealed class FuncionarioRepository : GenericRepository<FuncionarioModel>, IFuncionarioRepository, IFuncionarioReport
    {
        public FuncionarioRepository()
        {
            EmpresaModel empresa = new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");

            ServicoModel servico1 = new ServicoModel(empresa, "Corte", 25, true);
            ServicoModel servico2 = new ServicoModel(empresa, "Luzes", 60, true);

            Add(new FuncionarioModel(empresa, "Leo Funcionario 5", new DateTime(1994, 5, 7), "(15) 4555-1463", "(41) 97164-5267", "leo5@gmail.com", "M", new List<ServicoModel> { servico1, servico2 }));
            Add(new FuncionarioModel(empresa, "Leo Funcionario 4", new DateTime(1994, 5, 7), "(15) 4555-1463", "(41) 97164-5267", "leo4@gmail.com", "M", new List<ServicoModel> { servico1, servico2 }));
            Add(new FuncionarioModel(empresa, "Leo Funcionario 3", new DateTime(1994, 5, 7), "(15) 4555-1463", "(41) 97164-5267", "leo3@gmail.com", "M", new List<ServicoModel> { servico1, servico2 }));
            Add(new FuncionarioModel(empresa, "Leo Funcionario 2", new DateTime(1994, 5, 7), "(15) 4555-1463", "(41) 97164-5267", "leo2@gmail.com", "M", new List<ServicoModel> { servico1, servico2 }));
            Add(new FuncionarioModel(empresa, "Leo Funcionario 1", new DateTime(1994, 5, 7), "(15) 4555-1463", "(41) 97164-5267", "leo1@gmail.com", "M", new List<ServicoModel> { servico1, servico2 }));

        }
        public override FuncionarioModel Find(long idEmpresa, string nome)
        {
            return this.SingleOrDefault(o => o.Nome == nome && o.IdEmpresa == idEmpresa);
        }
        public List<FiltrosReportView> ListarFiltros(long idEmpresa, bool? deletado)
        {
            List<FiltrosReportView> itens = new List<FiltrosReportView>();
            foreach (FuncionarioModel item in this.List(idEmpresa).Where(s => (deletado == null || s.Deletado == deletado)).ToList())
            {
                itens.Add(new FiltrosReportView(item.Id, item.Nome));
            }
            return itens;
        }
    }
}
