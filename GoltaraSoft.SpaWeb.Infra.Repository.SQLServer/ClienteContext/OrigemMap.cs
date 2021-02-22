using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ClienteContext
{
    public class OrigemMap : EntityTypeConfiguration<OrigemModel>
    {
        public OrigemMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().

            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("Origem", "Cliente");

            //Descricao terá no máximo 15 caracteres e será um campo "NOT NULL"
            Property(x => x.Nome).IsUnique("UK_Nome", 1);
            Property(x => x.IdEmpresa).IsUnique("UK_Nome", 2);
        }
    }
}
