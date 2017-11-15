
using System;
using System.Collections.Generic;
using HealthyProject.Models.Metadata;
using System.ComponentModel.DataAnnotations;

namespace HealthyProject.Models
{
    using System;
    using System.Collections.Generic;
    using HealthyProject.Models.Metadata;
    using System.ComponentModel.DataAnnotations;




    public class Eutilizador
    {
       public int UserID { get; set; }

        [Required(ErrorMessage = "Introduza o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza o género")]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Introduza a Data de nascimento")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Data_nascimento { get; set; }

        [Required(ErrorMessage = "Introduza o peso (kg)")]
        public Nullable<int> Peso { get; set; }

        [Required(ErrorMessage = "Introduza a altura (centímetros)")]
        public Nullable<int> Altura { get; set; }


        [Required(ErrorMessage = "Introduza o índice de atividade física ")]
        [Display(Name = "Índice de atividade física/ Frequência")]
        public Nullable<double> Actividade_fisica { get; set; }

        [Display(Name = "Número de horas de sono diárias")]
        public Nullable<int> Nr_horas_sono { get; set; }

        [Display(Name = "Número de refeições diárias")]
        public Nullable<int> Nr_refeicoes { get; set; }
        [Display(Name = "Hábitos alcoólicos")]
        public Nullable<bool> Habitos_alcoolicos { get; set; }

        [Display(Name = "Índice de massa muscular (%)")]
        public Nullable<double> MMuscular { get; set; }

        [Display(Name = "Índice de massa gorda (%)")]
        public Nullable<double> MGorda { get; set; }


    }
}