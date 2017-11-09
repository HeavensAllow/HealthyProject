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

    [MetadataType(typeof(ObjectivoMetadata))]
    public partial class Objectivo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Objectivo()
        {
            this.RegistoDiarios = new HashSet<RegistoDiario>();
        }
    
        public int ObjectivoID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> Data_inicio { get; set; }
        public Nullable<System.DateTime> Data_fim { get; set; }
        public Nullable<int> Peso_objectivo { get; set; }
        public Nullable<int> Intake_diarioR { get; set; }
        public Nullable<int> Intake_diarioA { get; set; }
        public Nullable<int> Peso_Final { get; set; }
    
        public virtual Utilizador Utilizador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistoDiario> RegistoDiarios { get; set; }
    }
}
