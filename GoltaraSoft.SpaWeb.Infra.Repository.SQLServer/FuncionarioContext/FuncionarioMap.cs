using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Maps
{
    public class FuncionarioMap : EntityTypeConfiguration<FuncionarioModel>
    {
        public FuncionarioMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().

            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("Funcionario", "Funcionario");

            //Descricao terá no máximo 15 caracteres e será um campo "NOT NULL"
            Property(x => x.Nome).IsUnique("UK_Nome", 1);
            Property(x => x.IdEmpresa).IsUnique("UK_Nome", 2);

            Property(x => x.Email).IsUnique("UK_Email", 1);
            Property(x => x.IdEmpresa).IsUnique("UK_Email", 2);

            HasRequired(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.IdEmpresa);

            /*Funcionarios tem uma lista de Servicos*/
            HasMany(x => x.Servicos)
                .WithMany(x => x.Funcionarios) //...e Serviço uma lista de Funcionarios
                .Map(m =>   //esse relacionamento será mapeado em uma terceira tabela
                {
                    m.MapLeftKey("FuncionarioId");  //chave da esquerda será de FuncionarioId
                    m.MapRightKey("ServicoId"); //Chave da direita será ServicoId
                    m.ToTable("FuncionariosServicos", "Funcionario"); // e o nome da tabela será FuncionariosServicos
                });
        }
    }
}
