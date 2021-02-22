namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizarModelagens : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Cliente.Cliente", "IdOrigem", "Cliente.Origem");
            AddColumn("Cliente.Cliente", "OrigemModel_Id", c => c.Long());
            CreateIndex("Cliente.Cliente", "OrigemModel_Id");
            AddForeignKey("Cliente.Cliente", "OrigemModel_Id", "Cliente.Origem", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Cliente.Cliente", "OrigemModel_Id", "Cliente.Origem");
            DropIndex("Cliente.Cliente", new[] { "OrigemModel_Id" });
            DropColumn("Cliente.Cliente", "OrigemModel_Id");
            AddForeignKey("Cliente.Cliente", "IdOrigem", "Cliente.Origem", "Id");
        }
    }
}
