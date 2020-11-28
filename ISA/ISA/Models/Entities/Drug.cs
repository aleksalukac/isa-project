﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISA.Models.Entities
{
    [Table("tbDrugs")]
    public class Drug : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DrugType Type { get; set; }
        public DrugForm Form { get; set; }
        public string Ingredients { get; set; }
        public string Drugmaker { get; set; }
        public bool IsPrescribable { get; set; }
        public List<Drug> SimilarDrugs { get; set; } 
        public string Notes { get; set; }
    }

    public class DrugType
    {
        public long Id { get; set; }
        static List<string> DrugTypes = new List<string>() {"antibiotics", "antivirals"};
    }

    public enum DrugForm
    {
        Pill,
        Syrup,
        Tablet,
        ThinFilm,
        LiquidSolution,
        Powder,
        Seed,
        Pastes,
        BuccalFilm
    }
}
