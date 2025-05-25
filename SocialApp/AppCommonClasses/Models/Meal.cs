using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Enums;

namespace AppCommonClasses.Models
{

    public class Meal
    {
        [Key]
        [Column("m_id")]
        public int Id { get; set; }

        [Column("m_name")]
        public string Name { get; set; }

        public string Ingredients { get; set; }

        [Column("calories")]
        public double Calories { get; set; }


        public string Category { get; set; }

        public double Protein { get; set; }

        public double Carbohydrates { get; set; }

        public double Fat { get; set; }

        [Column("photo_link")]
        public string PhotoLink { get; set; }

        public string Description { get; set; }

        [Column("preparation_time")]
        public double PreparationTime { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public Meal(string name, string ingredients, int calories, string category, string photoLink, string description, double protein, double fat, double carbohydrates, double preparationTime)
        {
            Name = name;
            Ingredients = ingredients;
            Calories = calories;
            PhotoLink = photoLink;
            Description = description;
            Protein = protein;
            Carbohydrates = carbohydrates;
            Fat = fat;
            PreparationTime = preparationTime;
        }
    }

    public enum MealModel
    {
        SuccessfulCreationIndicator = 0,
        FailedOperationCode = -1,
    }
}