using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Models
{
    public class FuncionarioEditViewModel : PessoaViewModel
    {
        public FuncionarioEditViewModel()
        {
            ServicosPrestados = new List<ServicoModel>();
            DataNascimento = new DateTime(1980, 01, 01);
        }
        public long[] ServicosSelecionados { get; set; }
        public MultiSelectList ServicosPossiveis { get; set; }
        public List<ServicoModel> ServicosPrestados { get; private set; }
    }
}
