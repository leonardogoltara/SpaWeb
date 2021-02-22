using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.PessoaContext.Pessoa;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.FuncionarioContext
{
    public class FuncionarioModel : PessoaModel
    {
        public ICollection<ServicoModel> Servicos { get; set; }

        public FuncionarioModel()
        {        }
        public FuncionarioModel(EmpresaModel empresa, string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, List<ServicoModel> servicos) 
            : base(empresa)
        {
            Fill(nome, dataNascimento, telefone, celular, email, sexo, servicos);
        }
        internal void Editar(string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, List<ServicoModel> servicos)
        {
            Fill(nome, dataNascimento, telefone, celular, email, sexo, servicos);
        }
        internal void Fill(string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, List<ServicoModel> servicos)
        {
            // Serviços não são mais obrigatórios.
            //if (servicos.IsNull() || servicos.Count == 0)
            //    throw new FuncionarioInvalidoException("Funcionário sem nenhum serviço relacionado.");

            Fill(nome, dataNascimento, telefone, celular, email, sexo);

            Servicos = servicos;

        }

        public override bool Equals(object obj)
        {
            var other = obj as FuncionarioModel;
            if (other.IsNull()) return false;

            return Nome == other.Nome;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static implicit operator string(FuncionarioModel obj)
        {
            return obj.Nome;
        }
    }
}
