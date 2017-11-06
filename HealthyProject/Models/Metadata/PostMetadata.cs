using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public partial class PostMetadata
    {
        [Required(ErrorMessage = "Por favor insira o título")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Por favor insira o texto")]
        public string Texto { get; set; }
        [Required(AllowEmptyStrings = true, ErrorMessage = "Por favor insira o link")]
        public string Link { get; set; }
    }
}