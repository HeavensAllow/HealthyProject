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
    
    public partial class Ingrediente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingrediente()
        {
            this.RefeicaoIngredientes = new HashSet<RefeicaoIngrediente>();
        }
    
        public int IngredientesID { get; set; }
        public string Categoria { get; set; }
        public string Nome { get; set; }
        public int Unidade { get; set; }
        public int Kcal { get; set; }
        public int Proteinas { get; set; }
        public int Gordura { get; set; }
        public int HC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefeicaoIngrediente> RefeicaoIngredientes { get; set; }
    }
}
