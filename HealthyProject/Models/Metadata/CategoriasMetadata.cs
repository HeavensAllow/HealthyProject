﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public class CategoriasMetadata
    {
        [Required(ErrorMessage = "Por favor insira o nome")]
        public string Nome { get; set; }
    }
}
