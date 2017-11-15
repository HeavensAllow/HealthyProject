using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public class ObjectivoMetadata
    {
        [Required(ErrorMessage = "Por favor introduza a data a dar início ao objectivo")]
        public Nullable<System.DateTime> Data_inicio { get; set; }
        [Required(ErrorMessage = "Por favor introduza o peso objectivo a atingir")]
        public Nullable<int> Peso_objectivo { get; set; }
    }
}
