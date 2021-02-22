using System;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews
{
    public class AniversarianteReportView
    {
        public AniversarianteReportView()
        {

        }
        public AniversarianteReportView(string nome, string dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
        }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }

        public static implicit operator List<object>(AniversarianteReportView v)
        {
            throw new NotImplementedException();
        }
    }
}
