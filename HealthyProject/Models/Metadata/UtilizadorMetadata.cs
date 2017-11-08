﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyProject.Models.Metadata
{
    public class UtilizadorMetadata
    {
        [Required(ErrorMessage ="Introduza o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza o Genero")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "Introduza a Data de Nascimento")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Data_nascimento { get; set; }

        [Required(ErrorMessage = "Introduza o Peso (kg)")]
        public Nullable<int> Peso { get; set; }

        [Required(ErrorMessage = "Introduza a Altura (metros)")]
        public Nullable<int> Altura { get; set; }

        
        [Required(ErrorMessage = "Introduza o Indice de Atividade Fisica")]
        [Display(Name = "Indice de Atividade Fisica (Escala de 0 a 4)")]
        public Nullable<double> Actividade_fisica { get; set; }

        [Display(Name = "Numero de Horas de Sono Diarias")]
        public Nullable<int> Nr_horas_sono { get; set; }

        [Display(Name = "Numero de Refeicoes Diarias")]
        public Nullable<int> Nr_refeicoes { get; set; }
        [Display(Name = "Habitos Alcoolicos")]
        public Nullable<bool> Habitos_alcoolicos { get; set; }

        [Display(Name = "Indice de Massa Muscular (%)")]
        public Nullable<double> MMuscular { get; set; }

        [Display(Name = "Indice de Massa Gorda (%)")]
        public Nullable<double> MGorda { get; set; }
    }
}
