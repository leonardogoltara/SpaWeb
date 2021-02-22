using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.Validators;

namespace GoltaraSolutions.SpaWeb.Domain.EmpresaContext
{
    public class EmpresaModel : Aggregate
    {
        public string CNPJ { get; private set; }
        public string Nome { get; private set; }
        public EmpresaResponsavelCobranca ResponsavelCobranca { get; private set; }

        public EmpresaModel() { }
        public EmpresaModel(string cnpj,
            string nome,
            string nomeResponsavel,
            string telefoneResponsavel,
            string emailResponsavel)
        {
            Fill(cnpj, nome, nomeResponsavel, telefoneResponsavel, emailResponsavel);
        }
        internal void Editar(string cnpj,
            string nome,
            string nomeResponsavel,
            string telefoneResponsavel,
            string emailResponsavel)
        {
            Fill(cnpj, nome, nomeResponsavel, telefoneResponsavel, emailResponsavel);
        }
        /// <summary>
        /// Valida e preenche.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="nome"></param>
        internal void Fill(string cnpj,
            string nome,
            string nomeResponsavel,
            string telefoneResponsavel,
            string emailResponsavel)
        {
            if (cnpj.IsNullOrEmptyString() || !CommonValidator.ValidCNPJ(cnpj))
                throw new EmpresaInvalidoException($"CNPJ {cnpj} inválido.");

            if (nome.IsNullOrEmptyString())
                throw new EmpresaInvalidoException("Nome inválido.");

            ResponsavelCobranca = new EmpresaResponsavelCobranca(nomeResponsavel, 
                telefoneResponsavel, 
                emailResponsavel);

            Nome = nome.Value();
            CNPJ = cnpj.Value();
        }

        internal void Deletar()
        {
            Deletado = true;
        }

        internal void Recuperar()
        {
            Deletado = false;
        }

        public override bool Equals(object obj)
        {
            var other = obj as EmpresaModel;
            if (other.IsNull()) return false;

            return Nome == other.Nome;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static implicit operator string(EmpresaModel obj)
        {
            return obj.Nome;
        }
    }
}
