namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Agenda.Atendimento",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DataHora = c.DateTime(nullable: false),
                        IdCliente = c.Long(nullable: false),
                        IdServico = c.Long(nullable: false),
                        IdFuncionario = c.Long(nullable: false),
                        GuidUsuarioAgendou = c.String(maxLength: 150, unicode: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cancelado = c.Boolean(nullable: false),
                        Concluido = c.Boolean(nullable: false),
                        Confirmado = c.Boolean(nullable: false),
                        IdEmpresa = c.Long(nullable: false),
                        Deletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Cliente.Cliente", t => t.IdCliente)
                .ForeignKey("Empresa.Empresa", t => t.IdEmpresa)
                .ForeignKey("Funcionario.Funcionario", t => t.IdFuncionario)
                .ForeignKey("Servico.Servico", t => t.IdServico)
                .Index(t => t.IdCliente)
                .Index(t => t.IdServico)
                .Index(t => t.IdFuncionario)
                .Index(t => t.IdEmpresa);
            
            CreateTable(
                "Cliente.Cliente",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdOrigem = c.Long(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Sexo = c.String(maxLength: 150, unicode: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Telefone = c.String(maxLength: 150, unicode: false),
                        Celular = c.String(maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        IdEmpresa = c.Long(nullable: false),
                        Deletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Empresa.Empresa", t => t.IdEmpresa)
                .ForeignKey("Cliente.Origem", t => t.IdOrigem)
                .Index(t => t.IdOrigem)
                .Index(t => t.Nome, unique: true, name: "UK_Nome")
                .Index(t => new { t.Email, t.IdEmpresa }, unique: true, name: "UK_Email");
            
            CreateTable(
                "Empresa.Empresa",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CNPJ = c.String(nullable: false, maxLength: 150, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Deletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CNPJ, unique: true, name: "UK_CNPJ")
                .Index(t => t.Nome, unique: true, name: "UK_Nome");
            
            CreateTable(
                "Funcionario.Funcionario",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Sexo = c.String(maxLength: 150, unicode: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Telefone = c.String(maxLength: 150, unicode: false),
                        Celular = c.String(maxLength: 150, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        IdEmpresa = c.Long(nullable: false),
                        Deletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Empresa.Empresa", t => t.IdEmpresa)
                .Index(t => t.Nome, unique: true, name: "UK_Nome")
                .Index(t => new { t.Email, t.IdEmpresa }, unique: true, name: "UK_Email");
            
            CreateTable(
                "Servico.Servico",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoFixo = c.Boolean(nullable: false),
                        IdEmpresa = c.Long(nullable: false),
                        Deletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Empresa.Empresa", t => t.IdEmpresa)
                .Index(t => new { t.Nome, t.IdEmpresa }, unique: true, name: "UK_Nome");
            
            CreateTable(
                "Cliente.Origem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        IdEmpresa = c.Long(nullable: false),
                        Deletado = c.Boolean(nullable: false),
                        Empresa_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Empresa.Empresa", t => t.Empresa_Id)
                .Index(t => new { t.Nome, t.IdEmpresa }, unique: true, name: "UK_Nome")
                .Index(t => t.Empresa_Id);
            
            CreateTable(
                "Produto.Produto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdEmpresa = c.Long(nullable: false),
                        Deletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Empresa.Empresa", t => t.IdEmpresa)
                .Index(t => new { t.Nome, t.IdEmpresa }, unique: true, name: "UK_Nome");
            
            CreateTable(
                "Empresa.ResponsavelCobranca",
                c => new
                    {
                        IdEmpresa = c.Long(nullable: false),
                        Nome = c.String(maxLength: 150, unicode: false),
                        Telefone = c.String(maxLength: 150, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.IdEmpresa)
                .ForeignKey("Empresa.Empresa", t => t.IdEmpresa, cascadeDelete: true)
                .Index(t => t.IdEmpresa);
            
            CreateTable(
                "Funcionario.FuncionariosServicos",
                c => new
                    {
                        FuncionarioId = c.Long(nullable: false),
                        ServicoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.FuncionarioId, t.ServicoId })
                .ForeignKey("Funcionario.Funcionario", t => t.FuncionarioId)
                .ForeignKey("Servico.Servico", t => t.ServicoId)
                .Index(t => t.FuncionarioId)
                .Index(t => t.ServicoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Agenda.Atendimento", "IdServico", "Servico.Servico");
            DropForeignKey("Agenda.Atendimento", "IdFuncionario", "Funcionario.Funcionario");
            DropForeignKey("Agenda.Atendimento", "IdEmpresa", "Empresa.Empresa");
            DropForeignKey("Agenda.Atendimento", "IdCliente", "Cliente.Cliente");
            DropForeignKey("Cliente.Cliente", "IdOrigem", "Cliente.Origem");
            DropForeignKey("Cliente.Cliente", "IdEmpresa", "Empresa.Empresa");
            DropForeignKey("Empresa.ResponsavelCobranca", "IdEmpresa", "Empresa.Empresa");
            DropForeignKey("Produto.Produto", "IdEmpresa", "Empresa.Empresa");
            DropForeignKey("Cliente.Origem", "Empresa_Id", "Empresa.Empresa");
            DropForeignKey("Funcionario.FuncionariosServicos", "ServicoId", "Servico.Servico");
            DropForeignKey("Funcionario.FuncionariosServicos", "FuncionarioId", "Funcionario.Funcionario");
            DropForeignKey("Servico.Servico", "IdEmpresa", "Empresa.Empresa");
            DropForeignKey("Funcionario.Funcionario", "IdEmpresa", "Empresa.Empresa");
            DropIndex("Funcionario.FuncionariosServicos", new[] { "ServicoId" });
            DropIndex("Funcionario.FuncionariosServicos", new[] { "FuncionarioId" });
            DropIndex("Empresa.ResponsavelCobranca", new[] { "IdEmpresa" });
            DropIndex("Produto.Produto", "UK_Nome");
            DropIndex("Cliente.Origem", new[] { "Empresa_Id" });
            DropIndex("Cliente.Origem", "UK_Nome");
            DropIndex("Servico.Servico", "UK_Nome");
            DropIndex("Funcionario.Funcionario", "UK_Email");
            DropIndex("Funcionario.Funcionario", "UK_Nome");
            DropIndex("Empresa.Empresa", "UK_Nome");
            DropIndex("Empresa.Empresa", "UK_CNPJ");
            DropIndex("Cliente.Cliente", "UK_Email");
            DropIndex("Cliente.Cliente", "UK_Nome");
            DropIndex("Cliente.Cliente", new[] { "IdOrigem" });
            DropIndex("Agenda.Atendimento", new[] { "IdEmpresa" });
            DropIndex("Agenda.Atendimento", new[] { "IdFuncionario" });
            DropIndex("Agenda.Atendimento", new[] { "IdServico" });
            DropIndex("Agenda.Atendimento", new[] { "IdCliente" });
            DropTable("Funcionario.FuncionariosServicos");
            DropTable("Empresa.ResponsavelCobranca");
            DropTable("Produto.Produto");
            DropTable("Cliente.Origem");
            DropTable("Servico.Servico");
            DropTable("Funcionario.Funcionario");
            DropTable("Empresa.Empresa");
            DropTable("Cliente.Cliente");
            DropTable("Agenda.Atendimento");
        }
    }
}
