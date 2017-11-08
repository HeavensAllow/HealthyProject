using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public class ComentariosMetadata
    {
        [Required(ErrorMessage = "Por favor insira o comentário")]
        public string Comment { get; set; }
    }
}