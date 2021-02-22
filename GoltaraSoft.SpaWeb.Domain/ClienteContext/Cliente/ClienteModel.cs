using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.PessoaContext.Pessoa;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    public class ClienteModel : PessoaModel
    {
        public long IdOrigem { get; private set; }
        public OrigemModel Origem { get; private set; }

        public ClienteModel() : base()
        {

        }
        public ClienteModel(EmpresaModel empresa, string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, OrigemModel origem)
            : base(empresa)

        {
            Fill(nome, dataNascimento, telefone, celular, email, sexo, origem);
        }

        public void Editar(string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, OrigemModel origem)
        {
            Fill(nome, dataNascimento, telefone, celular, email, sexo, origem);
        }
        public void Fill(string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo, OrigemModel origem)
        {
            if (origem.IsNull())
                throw new ClienteInvalidoException("Cliente sem origem.");

            Fill(nome, dataNascimento, telefone, celular, email, sexo);

            Origem = null;
            IdOrigem = origem.Id;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ClienteModel;
            if (other.IsNull()) return false;

            return Nome == other.Nome;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static implicit operator string(ClienteModel obj)
        {
            return obj.Nome;
        }
    }
}