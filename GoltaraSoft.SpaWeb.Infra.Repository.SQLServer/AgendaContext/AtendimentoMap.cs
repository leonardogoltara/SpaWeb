using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.AgendaContext
{
    public class AtendimentoMap : EntityTypeConfiguration<AtendimentoModel>
    {
        public AtendimentoMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().

            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("Atendimento", "Agenda");


            Property(x => x.IdCliente).IsRequired();
            Property(x => x.IdFuncionario).IsRequired();
            Property(x => x.IdServico).IsRequired();
            Property(x => x.DataHora).IsRequired();

            HasRequired(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.IdEmpresa);

            HasRequired(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.IdCliente);

            HasRequired(x => x.Funcionario)
                .WithMany()
                .HasForeignKey(x => x.IdFuncionario);
                      
            HasRequired(x => x.Servico)
                .WithMany()
                .HasForeignKey(x => x.IdServico);
        }
    }
}
