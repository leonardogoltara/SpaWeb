namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovendoObrigatoriedades : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Cliente.Cliente", "DataNascimento", c => c.DateTime());
            AlterColumn("Funcionario.Funcionario", "DataNascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("Funcionario.Funcionario", "DataNascimento", c => c.DateTime(nullable: false));
            AlterColumn("Cliente.Cliente", "DataNascimento", c => c.DateTime(nullable: false));
        }
    }
}
