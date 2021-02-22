using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;

namespace GoltaraSolutions.SpaWeb.Domain.ProdutoContext
{
    public class ProdutoModel : Aggregate
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public EmpresaModel Empresa { get; private set; }
        public long IdEmpresa { get; private set; }

        public ProdutoModel()
        {

        }
        public ProdutoModel(EmpresaModel empresa, string nome, decimal preco)
        {
            if (empresa.IsNull())
                throw new ProdutoInvalidoException("Empresa inválida.");

            Fill(nome, preco);

            Empresa = empresa;
            IdEmpresa = empresa.Id;
        }
        internal void Editar(string nome, decimal preco)
        {
            Fill(nome, preco);
        }
        internal void Fill(string nome, decimal preco)
        {
            if (preco <= 0)
                throw new ProdutoInvalidoException(string.Format("Preço {0} inválido.", preco));

            if (nome.IsNullOrEmptyString())
                throw new ProdutoInvalidoException("Nome inválido.");

            Nome = nome.Value();
            Preco = preco;
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
            var other = obj as ProdutoModel;
            if (other.IsNull()) return false;

            return Nome == other.Nome;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static implicit operator string(ProdutoModel obj)
        {
            return obj.Nome;
        }
    }
}
