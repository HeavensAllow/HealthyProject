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
    
    public partial class RegistoPeso
    {
        public int RegistoP_ID { get; set; }
        public int User_ID { get; set; }
        public Nullable<int> Peso { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
    
        public virtual Utilizador Utilizador { get; set; }
    }
}
