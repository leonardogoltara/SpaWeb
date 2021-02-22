using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.Validators;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.PessoaContext.Pessoa
{
    public class PessoaModel : Aggregate
    {
        public string Nome { get; private set; }
        public string Sexo { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string Email { get; private set; }
        public EmpresaModel Empresa { get; private set; }
        public long IdEmpresa { get; private set; }

        public PessoaModel()
        {

        }
        public PessoaModel(EmpresaModel empresa)
        {

            if (empresa.IsNull())
                throw new PessoaInvalidaException("Empresa inválida.");

            //Empresa = empresa;
            IdEmpresa = empresa.Id;
        }
        internal void Fill(string nome, DateTime? dataNascimento, string telefone, string celular, string email, string sexo)
        {
            if (nome.IsNullOrEmptyString())
                throw new FuncionarioInvalidoException("Nome inválido.");

            if (sexo.IsNullOrEmptyString())
                throw new FuncionarioInvalidoException("Sexo inválido.");

            //if (dataNascimento.IsNull() || dataNascimento.Year < 1900)
            //    throw new ContatoInvalidoException("Data de nascimento inválida.");

            if (dataNascimento.IsNotNull() && dataNascimento.Value.Year < 1900)
                throw new ContatoInvalidoException("Data de nascimento inválida.");

            bool bTel = false;
            bool bCel = false;

            if (!telefone.IsNullOrEmptyString())
            {
                if (!CommonValidator.ValidPhone(telefone))
                { throw new ContatoInvalidoException("Telefone inválido."); }
                else
                { bTel = true; }
            }

            if (!celular.IsNullOrEmptyString())
            {
                if (!CommonValidator.ValidPhone(celular))
                { throw new ContatoInvalidoException("Celular inválido."); }
                else { bCel = true; }
            }

            if (!bTel && !bCel)
                throw new ContatoInvalidoException("Preencha o telefone ou o celular.");

            // Se preencher tem que ser válido.
            if (email.IsNullOrEmptyString() || !CommonValidator.ValidEmail(email))
                throw new ContatoInvalidoException("E-mail inválido.");

            Telefone = telefone.Value();
            Celular = celular.Value();
            Email = email.Value();
            Sexo = sexo.Value();
            DataNascimento = dataNascimento;
            Nome = nome.Value();
        }
        internal void Deletar()
        {
            Deletado = true;
        }
        internal void Recuperar()
        {
            Deletado = false;
        }
    }
}