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
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.Comentarios = new HashSet<Comentario>();
        }
    
        public int PostID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Data { get; set; }
        public string Titulo { get; set; }
        public int SubcategoriaID { get; set; }
        public string Texto { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
