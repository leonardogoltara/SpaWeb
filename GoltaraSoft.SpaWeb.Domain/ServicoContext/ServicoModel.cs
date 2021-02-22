using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using System.Collections.Generic;

namespace GoltaraSolutions.SpaWeb.Domain.ServicoContext
{
    public class ServicoModel : Aggregate
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public bool PrecoFixo { get; private set; }
        // Apenas para o mapeamento.
        public long IdEmpresa { get; private set; }

        public ICollection<FuncionarioModel> Funcionarios { get; private set; }
        public EmpresaModel Empresa { get; private set; }


        public ServicoModel()
        {

        }
        public ServicoModel(EmpresaModel empresa, string nome, decimal preco, bool precoFixo)
        {
            if (empresa.IsNull())
                throw new ServicoInvalidoException("Empresa inválida.");

            Fill(nome, preco, precoFixo);

            Empresa = empresa;
            if (empresa.IsNotNull())
                IdEmpresa = empresa.Id;
        }
        internal void Editar(string nome, decimal preco, bool precoFixo)
        {
            Fill(nome, preco, precoFixo);
        }
        internal void Fill(string nome, decimal preco, bool precoFixo)
        {
            if (preco <= 0)
                throw new ServicoInvalidoException($"Preço {preco} inválido.");

            if (nome.IsNullOrEmptyString())
                throw new ServicoInvalidoException("Nome inválido.");


            Nome = nome.Value();
            Preco = preco;
            PrecoFixo = precoFixo;
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
            var other = obj as ServicoModel;
            if (other.IsNull()) return false;

            return Nome == other.Nome;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static implicit operator string(ServicoModel obj)
        {
            return obj.Nome;
        }
    }
}
