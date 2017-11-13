//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using HealthyProject.Models.Metadata;
    using System.ComponentModel.DataAnnotations;

   
   
    public partial class Utilizador
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilizador()
        {
            this.Objectivoes = new HashSet<Objectivo>();
            this.RegistoPesoes = new HashSet<RegistoPeso>();
        }
    
        public int UserID { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public Nullable<System.DateTime> Data_nascimento { get; set; }
        public Nullable<int> Peso { get; set; }
        public Nullable<int> Altura { get; set; }
        public Nullable<double> Actividade_fisica { get; set; }
        public Nullable<int> Nr_horas_sono { get; set; }
        public Nullable<int> Nr_refeicoes { get; set; }
        public Nullable<bool> Habitos_alcoolicos { get; set; }
        public Nullable<double> MMuscular { get; set; }
        public Nullable<double> MGorda { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Objectivo> Objectivoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistoPeso> RegistoPesoes { get; set; }
    }
}
