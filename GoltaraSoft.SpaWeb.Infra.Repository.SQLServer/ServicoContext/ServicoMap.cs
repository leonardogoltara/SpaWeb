using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ServicoContext
{
    public class ServicoMap : EntityTypeConfiguration<ServicoModel>
    {
        public ServicoMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().

            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("Servico", "Servico");

            //Descricao terá no máximo 15 caracteres e será um campo "NOT NULL"
            Property(x => x.Nome).IsUnique("UK_Nome", 1);
            Property(x => x.IdEmpresa).IsUnique("UK_Nome", 2);

            HasRequired(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.IdEmpresa);
        }
    }
}
