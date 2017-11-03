using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public partial class PostMetadata
    {
       
        public string Titulo { get; set; }
        
        public string Texto { get; set; }
    }
}