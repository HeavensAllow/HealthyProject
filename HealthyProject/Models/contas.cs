using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyProject.Models
{
    public class Contas
    {

       public double? total_kcal
        {
            get;
            set;
        }
        public double? total_proteinas
        {
            get;
            set;
        }

        public double? total_gordura
        {
            get;
            set;
        }

        public double? total_hidratos
        {
            get;
            set;
        }

        public string tipo
        {
            get;
            set;
        }
        public ICollection<RefeicaoBebida> listaBebidas
        {
            get;
            set;
        }
        public ICollection<RefeicaoPrato> listaPratos
        {
            get;
            set;
        }
        public ICollection<RefeicaoIngrediente> listaIngredientes
        {
            get;
            set;
        }
        public int refeicaoID
        {
            get;
            set;
        }
    }
}