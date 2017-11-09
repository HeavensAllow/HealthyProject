using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public class RefeicaoPratoMetadata
    {
        [Required(ErrorMessage = "Por favor indique qual a refeição se refere")]
        public int RefeicaoID { get; set; }
        [Required(ErrorMessage = "Por favor indique qual o prato ingerido")]
        public int PratoID { get; set; }
        [Required(ErrorMessage = "Por favor indique qual a dose em gramas referente ao prato ingerido")]
        public double Dose { get; set; }
    }
}