using System;
using System.Collections.Generic;
using System.Text;

namespace ISA.Models.Entities
{
    public class Drug : BaseEntity
    {
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
