using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Pharmacy.Models.DTO
{
    [Table("tbDrugs")]
    public class DrugDTO
    {
        public DrugDTO(long id, long drugAndQuantitiesId, string name, DrugType type, DrugForm form, string ingredients, string drugmaker, bool isPrescribable, double averageScore, string pharmacyName, double cost)
        {
            Id = id;
            DrugAndQuantitiesId = drugAndQuantitiesId;
            Name = name;
            Type = type;
            Form = form;
            Ingredients = ingredients;
            Drugmaker = drugmaker;
            IsPrescribable = isPrescribable;
            AverageScore = averageScore;
            PharmacyName = pharmacyName;
            Cost = cost;
        }

        public long Id { get; set; }
        public long DrugAndQuantitiesId { get; set; }
        public string Name { get; set; }
        public DrugType Type { get; set; }
        public DrugForm Form { get; set; }
        public string Ingredients { get; set; }
        public string Drugmaker { get; set; }
        public bool IsPrescribable { get; set; }
        public double AverageScore { get; set; }
        public string PharmacyName { get; set; }
        public double Cost { get; set; }
    }
}
