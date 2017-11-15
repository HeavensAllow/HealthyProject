using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthyProject.Models.Metadata
{
    public class RefeicaoIngredientesMetadata
    {
        public IEnumerable<SelectListItem> Categoria { get; set; }
        public string SelectedCategoria { get; set; }
        public IEnumerable<SelectListItem> Ingredientes { get; set; }
    }
}