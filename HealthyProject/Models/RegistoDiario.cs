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
    using System.ComponentModel.DataAnnotations;

    public partial class RegistoDiario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegistoDiario()
        {
            this.Refeicoes = new HashSet<Refeico>();
        }
    
        public int RegistoID { get; set; }
        public Nullable<int> ObjectivoID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Data { get; set; }
        [Display(Name = "Total de Kcal")]
        public int Total_Kcal { get; set; }
        [Display(Name = "Total de proteinas")]
        public int Total_proteinas { get; set; }
        [Display(Name = "Total de gorduras")]
        public int Total_gordura { get; set; }
        public int Total_HC { get; set; }
    
        public virtual Objectivo Objectivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Refeico> Refeicoes { get; set; }
    }
}
