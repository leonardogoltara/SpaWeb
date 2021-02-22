using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.AgendaContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ClienteContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.EmpresaContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Maps;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ProdutoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ServicoContext;
using GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Migrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer
{
    public class Contexto : DbContext
    {
        public Contexto() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;

            //Database.SetInitializer(new CreateDatabaseIfNotExists<Contexto>());
            //if (Database.Exists())
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contexto, Configuration>());
            //else
            //    Database.CreateIfNotExists();
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Contexto>());
            //Database.SetInitializer<Contexto>(new DropCreateDatabaseAlways<Contexto>());



        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Aqui vamos remover a pluralização padrão do Etity Framework que é em inglês
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*Desabilitamos o delete em cascata em relacionamentos 1:N evitando
             ter registros filhos     sem registros pai*/
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Basicamente a mesma configuração, porém em relacionamenos N:N
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            /*Toda propriedade do tipo string na entidade POCO
             seja configurado como VARCHAR no SQL Server*/
            modelBuilder.Properties<string>()
                      .Configure(p => p.HasColumnType("varchar"));

            /*Toda propriedade do tipo string na entidade POCO seja configurado como VARCHAR (150) no banco de dados */
            modelBuilder.Properties<string>()
                   .Configure(p => p.HasMaxLength(150));

            /*Definimos usando reflexão que toda propriedade que contenha "Nome da classe" + Id como "CursoId" por exemplo, seja dada como
                chave primária, caso não tenha sido especificado*/
            modelBuilder.Properties()
               .Where(p => p.Name == "Id")
               .Configure(p => p.IsKey().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));

            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new EmpresaResponsavelCobrancaMap());
            modelBuilder.Configurations.Add(new ServicoMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new OrigemMap());
            modelBuilder.Configurations.Add(new FuncionarioMap());
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new AtendimentoMap());

        }
        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string message = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        message += $"Class: {validationErrors.Entry.Entity.GetType().Name}" +
                            $" Property: {validationError.PropertyName}" +
                            $" Error: {validationError.ErrorMessage}" +
                            Environment.NewLine;

                        Logger.Log.Error("SaveChanges", message);
                    }
                }
                throw new DbEntityValidationException(message, dbEx);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;  // You can also choose to handle the exception here...
            }
        }
        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<EmpresaResponsavelCobranca> EmpresaResponsaveisCobranca { get; set; }
        public DbSet<ServicoModel> Servicos { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<OrigemModel> Origens { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<AtendimentoModel> Atendimentos { get; set; }
    }
}