
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/23/2017 11:49:11
-- Generated from EDMX file: C:\Users\altar11\Documents\GitHub\HealthyProject\HealthyProject\Models\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Healthy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Comentarios_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentarios] DROP CONSTRAINT [FK_Comentarios_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Comentarios_Posts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentarios] DROP CONSTRAINT [FK_Comentarios_Posts];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_Objectivo_Objectivo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Objectivo] DROP CONSTRAINT [FK_Objectivo_Objectivo];
GO
IF OBJECT_ID(N'[dbo].[FK_Opiniao_Comentarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Opiniao] DROP CONSTRAINT [FK_Opiniao_Comentarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Posts_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_Posts_Subcategorias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Subcategorias];
GO
IF OBJECT_ID(N'[dbo].[FK_RefeicaoBebida_Bebidas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RefeicaoBebida] DROP CONSTRAINT [FK_RefeicaoBebida_Bebidas];
GO
IF OBJECT_ID(N'[dbo].[FK_RefeicaoBebida_Refeicoes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RefeicaoBebida] DROP CONSTRAINT [FK_RefeicaoBebida_Refeicoes];
GO
IF OBJECT_ID(N'[dbo].[FK_RefeicaoIngredientes_Refeicoes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RefeicaoIngredientes] DROP CONSTRAINT [FK_RefeicaoIngredientes_Refeicoes];
GO
IF OBJECT_ID(N'[dbo].[FK_RefeicaoIngredientes_Refeicoes1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RefeicaoIngredientes] DROP CONSTRAINT [FK_RefeicaoIngredientes_Refeicoes1];
GO
IF OBJECT_ID(N'[dbo].[FK_RefeicaoPratos_Pratos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RefeicaoPratos] DROP CONSTRAINT [FK_RefeicaoPratos_Pratos];
GO
IF OBJECT_ID(N'[dbo].[FK_RefeicaoPratos_Pratos1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RefeicaoPratos] DROP CONSTRAINT [FK_RefeicaoPratos_Pratos1];
GO
IF OBJECT_ID(N'[dbo].[FK_Refeicoes_RegistoDiario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Refeicoes] DROP CONSTRAINT [FK_Refeicoes_RegistoDiario];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistoDiario_RegistoDiario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegistoDiario] DROP CONSTRAINT [FK_RegistoDiario_RegistoDiario];
GO
IF OBJECT_ID(N'[dbo].[FK_Subcategorias_Categorias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subcategorias] DROP CONSTRAINT [FK_Subcategorias_Categorias];
GO
IF OBJECT_ID(N'[dbo].[FK_Utilizador_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilizador] DROP CONSTRAINT [FK_Utilizador_AspNetUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Bebidas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bebidas];
GO
IF OBJECT_ID(N'[dbo].[Categorias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categorias];
GO
IF OBJECT_ID(N'[dbo].[Comentarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comentarios];
GO
IF OBJECT_ID(N'[dbo].[Ingredientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ingredientes];
GO
IF OBJECT_ID(N'[dbo].[Objectivo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Objectivo];
GO
IF OBJECT_ID(N'[dbo].[Opiniao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Opiniao];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Pratos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pratos];
GO
IF OBJECT_ID(N'[dbo].[RefeicaoBebida]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RefeicaoBebida];
GO
IF OBJECT_ID(N'[dbo].[RefeicaoIngredientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RefeicaoIngredientes];
GO
IF OBJECT_ID(N'[dbo].[RefeicaoPratos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RefeicaoPratos];
GO
IF OBJECT_ID(N'[dbo].[Refeicoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Refeicoes];
GO
IF OBJECT_ID(N'[dbo].[RegistoDiario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegistoDiario];
GO
IF OBJECT_ID(N'[dbo].[Subcategorias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subcategorias];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Utilizador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilizador];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Ingredientes'
CREATE TABLE [dbo].[Ingredientes] (
    [IngredientesID] int IDENTITY(1,1) NOT NULL,
    [Categoria] nvarchar(100)  NOT NULL,
    [Nome] nvarchar(100)  NOT NULL,
    [Unidade] int  NOT NULL,
    [Kcal] int  NOT NULL,
    [Proteinas] int  NOT NULL,
    [Gordura] int  NOT NULL,
    [HidCarbono] float  NOT NULL,
    [HC_Acucar] float  NOT NULL
);
GO

-- Creating table 'Objectivoes'
CREATE TABLE [dbo].[Objectivoes] (
    [ObjectivoID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [Data_inicio] datetime  NULL,
    [Data_fim] datetime  NULL,
    [Peso_objectivo] int  NULL,
    [Intake_diario] int  NOT NULL,
    [Intake_diarioR] int  NULL,
    [Intake_diarioA] int  NULL
);
GO

-- Creating table 'Pratos'
CREATE TABLE [dbo].[Pratos] (
    [PratosID] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(100)  NOT NULL,
    [Unidade] int  NOT NULL,
    [Kcal] int  NOT NULL,
    [Proteinas] int  NOT NULL,
    [Gordura] int  NOT NULL,
    [HC] int  NOT NULL,
    [Categoria] nvarchar(100)  NOT NULL,
    [HidCarbono] float  NOT NULL,
    [HC_Acucar] float  NOT NULL
);
GO

-- Creating table 'RefeicaoIngredientes'
CREATE TABLE [dbo].[RefeicaoIngredientes] (
    [IngredienteID] int  NOT NULL,
    [RefeicaoID] int  NOT NULL,
    [Quantidade] int  NOT NULL
);
GO

-- Creating table 'RefeicaoPratos'
CREATE TABLE [dbo].[RefeicaoPratos] (
    [RefeicaoID] int  NOT NULL,
    [PratoID] int  NOT NULL,
    [Dose] float  NOT NULL
);
GO

-- Creating table 'Refeicoes'
CREATE TABLE [dbo].[Refeicoes] (
    [RefeicaoID] int IDENTITY(1,1) NOT NULL,
    [RegistoID] int  NULL,
    [Data] datetime  NOT NULL,
    [Tipo] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'RegistoDiarios'
CREATE TABLE [dbo].[RegistoDiarios] (
    [RegistoID] int IDENTITY(1,1) NOT NULL,
    [ObjectivoID] int  NULL,
    [Data] datetime  NOT NULL,
    [Total_Kcal] int  NOT NULL,
    [Total_proteinas] int  NOT NULL,
    [Total_gordura] int  NOT NULL,
    [Total_HC] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Utilizadors'
CREATE TABLE [dbo].[Utilizadors] (
    [UserID] int  NOT NULL,
    [Nome] nvarchar(50)  NULL,
    [Genero] nvarchar(10)  NULL,
    [Data_nascimento] datetime  NULL,
    [Peso] int  NULL,
    [Altura] int  NULL,
    [Actividade_fisica] int  NULL,
    [Nr_horas_sono] int  NULL,
    [Nr_refeicoes] int  NULL,
    [Habitos_alcoolicos] bit  NULL,
    [MMuscular] float  NULL,
    [MGorda] float  NULL
);
GO

-- Creating table 'Bebidas'
CREATE TABLE [dbo].[Bebidas] (
    [BebidasID] int IDENTITY(1,1) NOT NULL,
    [Categoria] nvarchar(100)  NOT NULL,
    [Nome] nvarchar(100)  NOT NULL,
    [Unidade] int  NOT NULL,
    [Kcal] float  NOT NULL,
    [Proteinas] float  NOT NULL,
    [Gordura] float  NOT NULL,
    [HidCarbono] float  NOT NULL,
    [HC_Acucar] float  NOT NULL
);
GO

-- Creating table 'Categorias'
CREATE TABLE [dbo].[Categorias] (
    [CategoriaID] int  NOT NULL,
    [Nome] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Comentarios'
CREATE TABLE [dbo].[Comentarios] (
    [CommentID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Data] datetime  NOT NULL,
    [Comntario] varchar(max)  NOT NULL,
    [PostID] int  NOT NULL
);
GO

-- Creating table 'Opiniaos'
CREATE TABLE [dbo].[Opiniaos] (
    [commentID] int  NOT NULL,
    [userID] int  NOT NULL,
    [Opiniao1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [PostID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Data] datetime  NOT NULL,
    [Titulo] nvarchar(50)  NOT NULL,
    [SubcategoriaID] int  NOT NULL
);
GO

-- Creating table 'RefeicaoBebidas'
CREATE TABLE [dbo].[RefeicaoBebidas] (
    [BebidaID] int  NOT NULL,
    [RefeicaoID] int  NOT NULL,
    [Quantidade] int  NOT NULL
);
GO

-- Creating table 'Subcategorias'
CREATE TABLE [dbo].[Subcategorias] (
    [SubcategoriaID] int  NOT NULL,
    [Nome] nvarchar(50)  NOT NULL,
    [CategoriaID] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] int  NOT NULL,
    [AspNetUsers_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IngredientesID] in table 'Ingredientes'
ALTER TABLE [dbo].[Ingredientes]
ADD CONSTRAINT [PK_Ingredientes]
    PRIMARY KEY CLUSTERED ([IngredientesID] ASC);
GO

-- Creating primary key on [ObjectivoID] in table 'Objectivoes'
ALTER TABLE [dbo].[Objectivoes]
ADD CONSTRAINT [PK_Objectivoes]
    PRIMARY KEY CLUSTERED ([ObjectivoID] ASC);
GO

-- Creating primary key on [PratosID] in table 'Pratos'
ALTER TABLE [dbo].[Pratos]
ADD CONSTRAINT [PK_Pratos]
    PRIMARY KEY CLUSTERED ([PratosID] ASC);
GO

-- Creating primary key on [IngredienteID], [RefeicaoID] in table 'RefeicaoIngredientes'
ALTER TABLE [dbo].[RefeicaoIngredientes]
ADD CONSTRAINT [PK_RefeicaoIngredientes]
    PRIMARY KEY CLUSTERED ([IngredienteID], [RefeicaoID] ASC);
GO

-- Creating primary key on [RefeicaoID], [PratoID] in table 'RefeicaoPratos'
ALTER TABLE [dbo].[RefeicaoPratos]
ADD CONSTRAINT [PK_RefeicaoPratos]
    PRIMARY KEY CLUSTERED ([RefeicaoID], [PratoID] ASC);
GO

-- Creating primary key on [RefeicaoID] in table 'Refeicoes'
ALTER TABLE [dbo].[Refeicoes]
ADD CONSTRAINT [PK_Refeicoes]
    PRIMARY KEY CLUSTERED ([RefeicaoID] ASC);
GO

-- Creating primary key on [RegistoID] in table 'RegistoDiarios'
ALTER TABLE [dbo].[RegistoDiarios]
ADD CONSTRAINT [PK_RegistoDiarios]
    PRIMARY KEY CLUSTERED ([RegistoID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [UserID] in table 'Utilizadors'
ALTER TABLE [dbo].[Utilizadors]
ADD CONSTRAINT [PK_Utilizadors]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [BebidasID] in table 'Bebidas'
ALTER TABLE [dbo].[Bebidas]
ADD CONSTRAINT [PK_Bebidas]
    PRIMARY KEY CLUSTERED ([BebidasID] ASC);
GO

-- Creating primary key on [CategoriaID] in table 'Categorias'
ALTER TABLE [dbo].[Categorias]
ADD CONSTRAINT [PK_Categorias]
    PRIMARY KEY CLUSTERED ([CategoriaID] ASC);
GO

-- Creating primary key on [CommentID] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [PK_Comentarios]
    PRIMARY KEY CLUSTERED ([CommentID] ASC);
GO

-- Creating primary key on [commentID], [userID] in table 'Opiniaos'
ALTER TABLE [dbo].[Opiniaos]
ADD CONSTRAINT [PK_Opiniaos]
    PRIMARY KEY CLUSTERED ([commentID], [userID] ASC);
GO

-- Creating primary key on [PostID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([PostID] ASC);
GO

-- Creating primary key on [BebidaID], [RefeicaoID] in table 'RefeicaoBebidas'
ALTER TABLE [dbo].[RefeicaoBebidas]
ADD CONSTRAINT [PK_RefeicaoBebidas]
    PRIMARY KEY CLUSTERED ([BebidaID], [RefeicaoID] ASC);
GO

-- Creating primary key on [SubcategoriaID] in table 'Subcategorias'
ALTER TABLE [dbo].[Subcategorias]
ADD CONSTRAINT [PK_Subcategorias]
    PRIMARY KEY CLUSTERED ([SubcategoriaID] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [UserID] in table 'Utilizadors'
ALTER TABLE [dbo].[Utilizadors]
ADD CONSTRAINT [FK_Utilizador_AspNetUsers]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IngredienteID] in table 'RefeicaoIngredientes'
ALTER TABLE [dbo].[RefeicaoIngredientes]
ADD CONSTRAINT [FK_RefeicaoIngredientes_Refeicoes]
    FOREIGN KEY ([IngredienteID])
    REFERENCES [dbo].[Ingredientes]
        ([IngredientesID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserID] in table 'Objectivoes'
ALTER TABLE [dbo].[Objectivoes]
ADD CONSTRAINT [FK_Objectivo_Objectivo]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Utilizadors]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Objectivo_Objectivo'
CREATE INDEX [IX_FK_Objectivo_Objectivo]
ON [dbo].[Objectivoes]
    ([UserID]);
GO

-- Creating foreign key on [ObjectivoID] in table 'RegistoDiarios'
ALTER TABLE [dbo].[RegistoDiarios]
ADD CONSTRAINT [FK_RegistoDiario_RegistoDiario]
    FOREIGN KEY ([ObjectivoID])
    REFERENCES [dbo].[Objectivoes]
        ([ObjectivoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistoDiario_RegistoDiario'
CREATE INDEX [IX_FK_RegistoDiario_RegistoDiario]
ON [dbo].[RegistoDiarios]
    ([ObjectivoID]);
GO

-- Creating foreign key on [PratoID] in table 'RefeicaoPratos'
ALTER TABLE [dbo].[RefeicaoPratos]
ADD CONSTRAINT [FK_RefeicaoPratos_Pratos1]
    FOREIGN KEY ([PratoID])
    REFERENCES [dbo].[Pratos]
        ([PratosID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RefeicaoPratos_Pratos1'
CREATE INDEX [IX_FK_RefeicaoPratos_Pratos1]
ON [dbo].[RefeicaoPratos]
    ([PratoID]);
GO

-- Creating foreign key on [RefeicaoID] in table 'RefeicaoIngredientes'
ALTER TABLE [dbo].[RefeicaoIngredientes]
ADD CONSTRAINT [FK_RefeicaoIngredientes_Refeicoes1]
    FOREIGN KEY ([RefeicaoID])
    REFERENCES [dbo].[Refeicoes]
        ([RefeicaoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RefeicaoIngredientes_Refeicoes1'
CREATE INDEX [IX_FK_RefeicaoIngredientes_Refeicoes1]
ON [dbo].[RefeicaoIngredientes]
    ([RefeicaoID]);
GO

-- Creating foreign key on [RefeicaoID] in table 'RefeicaoPratos'
ALTER TABLE [dbo].[RefeicaoPratos]
ADD CONSTRAINT [FK_RefeicaoPratos_Pratos]
    FOREIGN KEY ([RefeicaoID])
    REFERENCES [dbo].[Refeicoes]
        ([RefeicaoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RegistoID] in table 'Refeicoes'
ALTER TABLE [dbo].[Refeicoes]
ADD CONSTRAINT [FK_Refeicoes_RegistoDiario]
    FOREIGN KEY ([RegistoID])
    REFERENCES [dbo].[RegistoDiarios]
        ([RegistoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refeicoes_RegistoDiario'
CREATE INDEX [IX_FK_Refeicoes_RegistoDiario]
ON [dbo].[Refeicoes]
    ([RegistoID]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [UserID] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_Comentarios_AspNetUsers]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comentarios_AspNetUsers'
CREATE INDEX [IX_FK_Comentarios_AspNetUsers]
ON [dbo].[Comentarios]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Posts_AspNetUsers]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Posts_AspNetUsers'
CREATE INDEX [IX_FK_Posts_AspNetUsers]
ON [dbo].[Posts]
    ([UserID]);
GO

-- Creating foreign key on [BebidaID] in table 'RefeicaoBebidas'
ALTER TABLE [dbo].[RefeicaoBebidas]
ADD CONSTRAINT [FK_RefeicaoBebida_Bebidas]
    FOREIGN KEY ([BebidaID])
    REFERENCES [dbo].[Bebidas]
        ([BebidasID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CategoriaID] in table 'Subcategorias'
ALTER TABLE [dbo].[Subcategorias]
ADD CONSTRAINT [FK_Subcategorias_Categorias]
    FOREIGN KEY ([CategoriaID])
    REFERENCES [dbo].[Categorias]
        ([CategoriaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Subcategorias_Categorias'
CREATE INDEX [IX_FK_Subcategorias_Categorias]
ON [dbo].[Subcategorias]
    ([CategoriaID]);
GO

-- Creating foreign key on [PostID] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_Comentarios_Posts]
    FOREIGN KEY ([PostID])
    REFERENCES [dbo].[Posts]
        ([PostID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comentarios_Posts'
CREATE INDEX [IX_FK_Comentarios_Posts]
ON [dbo].[Comentarios]
    ([PostID]);
GO

-- Creating foreign key on [commentID] in table 'Opiniaos'
ALTER TABLE [dbo].[Opiniaos]
ADD CONSTRAINT [FK_Opiniao_Comentarios]
    FOREIGN KEY ([commentID])
    REFERENCES [dbo].[Comentarios]
        ([CommentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SubcategoriaID] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_Posts_Subcategorias]
    FOREIGN KEY ([SubcategoriaID])
    REFERENCES [dbo].[Subcategorias]
        ([SubcategoriaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Posts_Subcategorias'
CREATE INDEX [IX_FK_Posts_Subcategorias]
ON [dbo].[Posts]
    ([SubcategoriaID]);
GO

-- Creating foreign key on [RefeicaoID] in table 'RefeicaoBebidas'
ALTER TABLE [dbo].[RefeicaoBebidas]
ADD CONSTRAINT [FK_RefeicaoBebida_Refeicoes]
    FOREIGN KEY ([RefeicaoID])
    REFERENCES [dbo].[Refeicoes]
        ([RefeicaoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RefeicaoBebida_Refeicoes'
CREATE INDEX [IX_FK_RefeicaoBebida_Refeicoes]
ON [dbo].[RefeicaoBebidas]
    ([RefeicaoID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------