using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using System.Data.Entity.ModelConfiguration;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.EmpresaContext
{
    public class EmpresaResponsavelCobrancaMap : EntityTypeConfiguration<EmpresaResponsavelCobranca>
    {
        public EmpresaResponsavelCobrancaMap()
        {
            //Definimos a propriedade Id como chave primária de forma genérica no OnModelCreating().
            HasKey(p => p.IdEmpresa);

            /*O método ToTable define qual o nome que será
              dado a tabela no banco de dados*/
            ToTable("ResponsavelCobranca", "Empresa");

        }
    }
}
