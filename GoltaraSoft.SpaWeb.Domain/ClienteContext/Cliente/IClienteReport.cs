using GoltaraSolutions.Common.Domain.Report;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    public interface IClienteReport : IReport<ClienteModel>
    {
        ICollection<AniversarianteReportView> AniversariantesMes(long idEmpresa);
    }
}
