using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyProject.Models
{
    public class Contas
    {

       public double? Total_kcal
        {
            get;
            set;
        }
        public double? Total_proteinas
        {
            get;
            set;
        }

        public double? Total_gordura
        {
            get;
            set;
        }

        public double? Total_hidratos
        {
            get;
            set;
        }

        public string Tipo
        {
            get;
            set;
        }
        public ICollection<RefeicaoBebida> ListaBebidas
        {
            get;
            set;
        }
        public ICollection<RefeicaoPrato> ListaPratos
        {
            get;
            set;
        }
        public ICollection<RefeicaoIngrediente> ListaIngredientes
        {
            get;
            set;
        }
        public int RefeicaoID
        {
            get;
            set;
        }
    }
}