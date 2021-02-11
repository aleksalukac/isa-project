using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Pharmacy.Models.Entities
{
    [Table("tbDrugs")]
    public class Drug
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DrugType Type { get; set; }
        public DrugForm Form { get; set; }
        public string Ingredients { get; set; }
        public string Drugmaker { get; set; }
        public bool IsPrescribable { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Drug> SimilarDrugs { get; set; }
        public string Notes { get; set; }
        public double AverageScore { get; set; }
        public List<AppUser> AllergicUsers { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
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
