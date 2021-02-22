using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.Validators;

namespace GoltaraSolutions.SpaWeb.Domain.EmpresaContext
{
    public class EmpresaResponsavelCobranca
    {
        //public long id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public EmpresaModel Empresa { get; private set; }
        public long IdEmpresa { get; private set; }
        public EmpresaResponsavelCobranca()
        {
        }

        public EmpresaResponsavelCobranca(string nome,
            string telefone,
            string email)
        {
            Fill(nome, telefone, email);
        }
        public void Editar(string nome,
            string telefone,
            string email)
        {
            Fill(nome, telefone, email);
        }
        public void Fill(string nome,
            string telefone,
            string email)
        {
            if (nome.IsNullOrEmptyString())
                throw new EmpresaInvalidoException("Nome do responsável inválido.");

            if (!CommonValidator.ValidPhone(telefone))
                throw new EmpresaInvalidoException("Telefone do responsável inválido.");

            if (!CommonValidator.ValidEmail(email))
                throw new EmpresaInvalidoException("E-mail do responsável inválido.");

            Nome = nome.Value();
            Telefone = telefone.Value();
            Email = email.Value();
        }
    }
}
