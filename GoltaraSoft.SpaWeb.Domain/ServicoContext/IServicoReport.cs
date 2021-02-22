using GoltaraSolutions.Common.Domain.Report;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ServicoContext
{
    public interface IServicoReport : IReport<ServicoModel>
    {
        IEnumerable<FiltrosReportView> FindByFuncionario(long idEmpresa, long idFuncionario);
    }
}
