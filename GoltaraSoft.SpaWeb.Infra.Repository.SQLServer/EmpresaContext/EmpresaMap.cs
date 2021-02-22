using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.EmpresaContext
{
    public class EmpresaMap : EntityTypeConfiguration<EmpresaModel>
    {
        public EmpresaMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().


            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("Empresa", "Empresa");

            //Descricao terá no máximo 15 caracteres e será um campo "NOT NULL"
            Property(x => x.CNPJ).IsUnique("UK_CNPJ", 1);
            Property(x => x.Nome).IsUnique("UK_Nome", 2);

            HasRequired(e => e.ResponsavelCobranca)
                .WithRequiredPrincipal(r => r.Empresa)
                .WillCascadeOnDelete(true);
        }
    }
}
