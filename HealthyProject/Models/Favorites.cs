using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyProject.Models
{
    public class Favorites
    {
        public int UserID { get; set; }
        public int PratoID { get; set; }
        public int Ocorrencias { get; set; }
        public string Nome { get; set; }
    }
}