-- <Migration ID="bdf99fe3-99f0-468e-888a-d88a3ae1861d" />
GO

PRINT N'Creating schemas'
GO
CREATE SCHEMA [Agenda]
AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Cliente]
AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Empresa]
AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Funcionario]
AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Produto]
AUTHORIZATION [dbo]
GO
CREATE SCHEMA [Servico]
AUTHORIZATION [dbo]
GO
PRINT N'Creating [Cliente].[Cliente]'
GO
CREATE TABLE [Cliente].[Cliente]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[IdOrigem] [bigint] NOT NULL,
[Nome] [varchar] (150) NOT NULL,
[Sexo] [varchar] (150) NULL,
[DataNascimento] [datetime] NULL,
[Telefone] [varchar] (150) NULL,
[Celular] [varchar] (150) NULL,
[Email] [varchar] (150) NOT NULL,
[IdEmpresa] [bigint] NOT NULL,
[Deletado] [bit] NOT NULL,
[OrigemModel_Id] [bigint] NULL
)
GO
PRINT N'Creating primary key [PK_Cliente.Cliente] on [Cliente].[Cliente]'
GO
ALTER TABLE [Cliente].[Cliente] ADD CONSTRAINT [PK_Cliente.Cliente] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [UK_Email] on [Cliente].[Cliente]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Email] ON [Cliente].[Cliente] ([Email], [IdEmpresa])
GO
PRINT N'Creating index [IX_IdOrigem] on [Cliente].[Cliente]'
GO
CREATE NONCLUSTERED INDEX [IX_IdOrigem] ON [Cliente].[Cliente] ([IdOrigem])
GO
PRINT N'Creating index [UK_Nome] on [Cliente].[Cliente]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Nome] ON [Cliente].[Cliente] ([Nome])
GO
PRINT N'Creating index [IX_OrigemModel_Id] on [Cliente].[Cliente]'
GO
CREATE NONCLUSTERED INDEX [IX_OrigemModel_Id] ON [Cliente].[Cliente] ([OrigemModel_Id])
GO
PRINT N'Creating [Agenda].[Atendimento]'
GO
CREATE TABLE [Agenda].[Atendimento]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[DataHora] [datetime] NOT NULL,
[IdCliente] [bigint] NOT NULL,
[IdServico] [bigint] NOT NULL,
[IdFuncionario] [bigint] NOT NULL,
[GuidUsuarioAgendou] [varchar] (150) NULL,
[Valor] [decimal] (18, 2) NOT NULL,
[Cancelado] [bit] NOT NULL,
[Concluido] [bit] NOT NULL,
[Confirmado] [bit] NOT NULL,
[IdEmpresa] [bigint] NOT NULL,
[Deletado] [bit] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Agenda.Atendimento] on [Agenda].[Atendimento]'
GO
ALTER TABLE [Agenda].[Atendimento] ADD CONSTRAINT [PK_Agenda.Atendimento] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [IX_IdCliente] on [Agenda].[Atendimento]'
GO
CREATE NONCLUSTERED INDEX [IX_IdCliente] ON [Agenda].[Atendimento] ([IdCliente])
GO
PRINT N'Creating index [IX_IdEmpresa] on [Agenda].[Atendimento]'
GO
CREATE NONCLUSTERED INDEX [IX_IdEmpresa] ON [Agenda].[Atendimento] ([IdEmpresa])
GO
PRINT N'Creating index [IX_IdFuncionario] on [Agenda].[Atendimento]'
GO
CREATE NONCLUSTERED INDEX [IX_IdFuncionario] ON [Agenda].[Atendimento] ([IdFuncionario])
GO
PRINT N'Creating index [IX_IdServico] on [Agenda].[Atendimento]'
GO
CREATE NONCLUSTERED INDEX [IX_IdServico] ON [Agenda].[Atendimento] ([IdServico])
GO
PRINT N'Creating [Empresa].[Empresa]'
GO
CREATE TABLE [Empresa].[Empresa]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[CNPJ] [varchar] (150) NOT NULL,
[Nome] [varchar] (150) NOT NULL,
[Deletado] [bit] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Empresa.Empresa] on [Empresa].[Empresa]'
GO
ALTER TABLE [Empresa].[Empresa] ADD CONSTRAINT [PK_Empresa.Empresa] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [UK_CNPJ] on [Empresa].[Empresa]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_CNPJ] ON [Empresa].[Empresa] ([CNPJ])
GO
PRINT N'Creating index [UK_Nome] on [Empresa].[Empresa]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Nome] ON [Empresa].[Empresa] ([Nome])
GO
PRINT N'Creating [Funcionario].[Funcionario]'
GO
CREATE TABLE [Funcionario].[Funcionario]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Nome] [varchar] (150) NOT NULL,
[Sexo] [varchar] (150) NULL,
[DataNascimento] [datetime] NULL,
[Telefone] [varchar] (150) NULL,
[Celular] [varchar] (150) NULL,
[Email] [varchar] (150) NOT NULL,
[IdEmpresa] [bigint] NOT NULL,
[Deletado] [bit] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Funcionario.Funcionario] on [Funcionario].[Funcionario]'
GO
ALTER TABLE [Funcionario].[Funcionario] ADD CONSTRAINT [PK_Funcionario.Funcionario] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [UK_Email] on [Funcionario].[Funcionario]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Email] ON [Funcionario].[Funcionario] ([Email], [IdEmpresa])
GO
PRINT N'Creating index [UK_Nome] on [Funcionario].[Funcionario]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Nome] ON [Funcionario].[Funcionario] ([Nome])
GO
PRINT N'Creating [Servico].[Servico]'
GO
CREATE TABLE [Servico].[Servico]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Nome] [varchar] (150) NOT NULL,
[Preco] [decimal] (18, 2) NOT NULL,
[PrecoFixo] [bit] NOT NULL,
[IdEmpresa] [bigint] NOT NULL,
[Deletado] [bit] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Servico.Servico] on [Servico].[Servico]'
GO
ALTER TABLE [Servico].[Servico] ADD CONSTRAINT [PK_Servico.Servico] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [UK_Nome] on [Servico].[Servico]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Nome] ON [Servico].[Servico] ([Nome], [IdEmpresa])
GO
PRINT N'Creating [Cliente].[Origem]'
GO
CREATE TABLE [Cliente].[Origem]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Nome] [varchar] (150) NOT NULL,
[IdEmpresa] [bigint] NOT NULL,
[Deletado] [bit] NOT NULL,
[Empresa_Id] [bigint] NULL
)
GO
PRINT N'Creating primary key [PK_Cliente.Origem] on [Cliente].[Origem]'
GO
ALTER TABLE [Cliente].[Origem] ADD CONSTRAINT [PK_Cliente.Origem] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [IX_Empresa_Id] on [Cliente].[Origem]'
GO
CREATE NONCLUSTERED INDEX [IX_Empresa_Id] ON [Cliente].[Origem] ([Empresa_Id])
GO
PRINT N'Creating index [UK_Nome] on [Cliente].[Origem]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Nome] ON [Cliente].[Origem] ([Nome], [IdEmpresa])
GO
PRINT N'Creating [Empresa].[ResponsavelCobranca]'
GO
CREATE TABLE [Empresa].[ResponsavelCobranca]
(
[IdEmpresa] [bigint] NOT NULL,
[Nome] [varchar] (150) NULL,
[Telefone] [varchar] (150) NULL,
[Email] [varchar] (150) NULL
)
GO
PRINT N'Creating primary key [PK_Empresa.ResponsavelCobranca] on [Empresa].[ResponsavelCobranca]'
GO
ALTER TABLE [Empresa].[ResponsavelCobranca] ADD CONSTRAINT [PK_Empresa.ResponsavelCobranca] PRIMARY KEY CLUSTERED  ([IdEmpresa])
GO
PRINT N'Creating index [IX_IdEmpresa] on [Empresa].[ResponsavelCobranca]'
GO
CREATE NONCLUSTERED INDEX [IX_IdEmpresa] ON [Empresa].[ResponsavelCobranca] ([IdEmpresa])
GO
PRINT N'Creating [Funcionario].[FuncionariosServicos]'
GO
CREATE TABLE [Funcionario].[FuncionariosServicos]
(
[FuncionarioId] [bigint] NOT NULL,
[ServicoId] [bigint] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Funcionario.FuncionariosServicos] on [Funcionario].[FuncionariosServicos]'
GO
ALTER TABLE [Funcionario].[FuncionariosServicos] ADD CONSTRAINT [PK_Funcionario.FuncionariosServicos] PRIMARY KEY CLUSTERED  ([FuncionarioId], [ServicoId])
GO
PRINT N'Creating index [IX_FuncionarioId] on [Funcionario].[FuncionariosServicos]'
GO
CREATE NONCLUSTERED INDEX [IX_FuncionarioId] ON [Funcionario].[FuncionariosServicos] ([FuncionarioId])
GO
PRINT N'Creating index [IX_ServicoId] on [Funcionario].[FuncionariosServicos]'
GO
CREATE NONCLUSTERED INDEX [IX_ServicoId] ON [Funcionario].[FuncionariosServicos] ([ServicoId])
GO
PRINT N'Creating [Produto].[Produto]'
GO
CREATE TABLE [Produto].[Produto]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Nome] [varchar] (150) NOT NULL,
[Preco] [decimal] (18, 2) NOT NULL,
[IdEmpresa] [bigint] NOT NULL,
[Deletado] [bit] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Produto.Produto] on [Produto].[Produto]'
GO
ALTER TABLE [Produto].[Produto] ADD CONSTRAINT [PK_Produto.Produto] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating index [UK_Nome] on [Produto].[Produto]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UK_Nome] ON [Produto].[Produto] ([Nome], [IdEmpresa])
GO
PRINT N'Adding foreign keys to [Agenda].[Atendimento]'
GO
ALTER TABLE [Agenda].[Atendimento] ADD CONSTRAINT [FK_Agenda.Atendimento_Cliente.Cliente_IdCliente] FOREIGN KEY ([IdCliente]) REFERENCES [Cliente].[Cliente] ([Id])
GO
ALTER TABLE [Agenda].[Atendimento] ADD CONSTRAINT [FK_Agenda.Atendimento_Servico.Servico_IdServico] FOREIGN KEY ([IdServico]) REFERENCES [Servico].[Servico] ([Id])
GO
ALTER TABLE [Agenda].[Atendimento] ADD CONSTRAINT [FK_Agenda.Atendimento_Funcionario.Funcionario_IdFuncionario] FOREIGN KEY ([IdFuncionario]) REFERENCES [Funcionario].[Funcionario] ([Id])
GO
ALTER TABLE [Agenda].[Atendimento] ADD CONSTRAINT [FK_Agenda.Atendimento_Empresa.Empresa_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [Empresa].[Empresa] ([Id])
GO
PRINT N'Adding foreign keys to [Cliente].[Cliente]'
GO
ALTER TABLE [Cliente].[Cliente] ADD CONSTRAINT [FK_Cliente.Cliente_Empresa.Empresa_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [Empresa].[Empresa] ([Id])
GO
ALTER TABLE [Cliente].[Cliente] ADD CONSTRAINT [FK_Cliente.Cliente_Cliente.Origem_OrigemModel_Id] FOREIGN KEY ([OrigemModel_Id]) REFERENCES [Cliente].[Origem] ([Id])
GO
PRINT N'Adding foreign keys to [Cliente].[Origem]'
GO
ALTER TABLE [Cliente].[Origem] ADD CONSTRAINT [FK_Cliente.Origem_Empresa.Empresa_Empresa_Id] FOREIGN KEY ([Empresa_Id]) REFERENCES [Empresa].[Empresa] ([Id])
GO
PRINT N'Adding foreign keys to [Empresa].[ResponsavelCobranca]'
GO
ALTER TABLE [Empresa].[ResponsavelCobranca] ADD CONSTRAINT [FK_Empresa.ResponsavelCobranca_Empresa.Empresa_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [Empresa].[Empresa] ([Id]) ON DELETE CASCADE
GO
PRINT N'Adding foreign keys to [Funcionario].[Funcionario]'
GO
ALTER TABLE [Funcionario].[Funcionario] ADD CONSTRAINT [FK_Funcionario.Funcionario_Empresa.Empresa_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [Empresa].[Empresa] ([Id])
GO
PRINT N'Adding foreign keys to [Produto].[Produto]'
GO
ALTER TABLE [Produto].[Produto] ADD CONSTRAINT [FK_Produto.Produto_Empresa.Empresa_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [Empresa].[Empresa] ([Id])
GO
PRINT N'Adding foreign keys to [Servico].[Servico]'
GO
ALTER TABLE [Servico].[Servico] ADD CONSTRAINT [FK_Servico.Servico_Empresa.Empresa_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [Empresa].[Empresa] ([Id])
GO
PRINT N'Adding foreign keys to [Funcionario].[FuncionariosServicos]'
GO
ALTER TABLE [Funcionario].[FuncionariosServicos] ADD CONSTRAINT [FK_Funcionario.FuncionariosServicos_Funcionario.Funcionario_FuncionarioId] FOREIGN KEY ([FuncionarioId]) REFERENCES [Funcionario].[Funcionario] ([Id])
GO
ALTER TABLE [Funcionario].[FuncionariosServicos] ADD CONSTRAINT [FK_Funcionario.FuncionariosServicos_Servico.Servico_ServicoId] FOREIGN KEY ([ServicoId]) REFERENCES [Servico].[Servico] ([Id])
GO
