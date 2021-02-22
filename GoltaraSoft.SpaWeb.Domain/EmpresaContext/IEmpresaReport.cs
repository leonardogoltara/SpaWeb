using GoltaraSolutions.Common.Domain.Report;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.EmpresaContext
{
    public interface IEmpresaReport //: IReport<EmpresaModel>
    {
        ICollection<EmpresaModel> List();

        List<FiltrosReportView> ListarFiltros(bool? deletado);

        EmpresaModel FindNome(string nome);
    }
}
