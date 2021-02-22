using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ClienteContext
{
    public class ClienteMap : EntityTypeConfiguration<ClienteModel>
    {
        public ClienteMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().

            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("Cliente", "Cliente");
            
            Property(x => x.Nome).IsUnique("UK_Nome", 1);
            Property(x => x.IdEmpresa).IsUnique("UK_Nome", 2);

            Property(x => x.Email).IsUnique("UK_Email", 1);
            Property(x => x.IdEmpresa).IsUnique("UK_Email", 2);

            Property(x => x.IdOrigem).IsRequired();

            HasRequired(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.IdEmpresa);

            //1:N - 1 cliente DEVE ter 1 origem e 1 origem pode ter muitos clientes
            HasRequired(x => x.Origem)
              .WithMany()
              .HasForeignKey(x => x.IdOrigem);
        }
    }
}
