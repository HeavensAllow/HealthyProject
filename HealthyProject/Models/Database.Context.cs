﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthyProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HealthyEntities1 : DbContext
    {
        public HealthyEntities1()
            : base("name=HealthyEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Bebida> Bebidas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Ingrediente> Ingredientes { get; set; }
        public virtual DbSet<Objectivo> Objectivoes { get; set; }
        public virtual DbSet<Opiniao> Opiniaos { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Prato> Pratos { get; set; }
        public virtual DbSet<RefeicaoBebida> RefeicaoBebidas { get; set; }
        public virtual DbSet<RefeicaoIngrediente> RefeicaoIngredientes { get; set; }
        public virtual DbSet<RefeicaoPrato> RefeicaoPratos { get; set; }
        public virtual DbSet<Refeico> Refeicoes { get; set; }
        public virtual DbSet<RegistoDiario> RegistoDiarios { get; set; }
        public virtual DbSet<Subcategoria> Subcategorias { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Utilizador> Utilizadors { get; set; }
    }
}
