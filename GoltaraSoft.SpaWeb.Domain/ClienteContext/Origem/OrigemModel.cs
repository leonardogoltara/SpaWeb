using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ClienteContext
{
    public class OrigemModel : Aggregate
    {
        public string Nome { get; private set; }
        public ICollection<ClienteModel> Clientes { get; private set; }
        public EmpresaModel Empresa { get; private set; }
        public long IdEmpresa { get; private set; }

        public OrigemModel()
        {

        }
        public OrigemModel(EmpresaModel empresa, string nome)
        {
            if (empresa.IsNull())
                throw new OrigemInvalidoException("Empresa inválida.");

            Fill(nome);

            Empresa = null;
            IdEmpresa = empresa.Id;
        }
        internal void Editar(string nome)
        {
            Fill(nome);
        }
        internal void Deletar()
        {
            Deletado = true;
        }
        internal void Recuperar()
        {
            Deletado = false;
        }
        internal void Fill(string nome)
        {
            if (nome.IsNullOrEmptyString())
                throw new OrigemInvalidoException("Nome inválido.");

            Nome = nome.Value();
        }

        public override bool Equals(object obj)
        {
            var other = obj as OrigemModel;
            if (other.IsNull()) return false;

            return Nome == other.Nome;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static implicit operator string(OrigemModel obj)
        {
            return obj.Nome;
        }
    }
}
