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

    [MetadataType(typeof(RefeicaoPratoMetadata))]

    public partial class RefeicaoPrato
    {
        public int RefeicaoID { get; set; }
        public int PratoID { get; set; }
        public double Dose { get; set; }
    
        public virtual Prato Prato { get; set; }
        public virtual Refeico Refeico { get; set; }
    }
}
