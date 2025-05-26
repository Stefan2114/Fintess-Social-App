using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Enums;



momentan asta ramane comentata
namespace AppCommonClasses.Models
{

    public class Meal
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        public string Ingredients { get; set; }

        [Column("calories")]
        public int Calories { get; set; }

        [Column("proteins")]
        public int Proteins { get; set; }

        [Column("carbohydrates")]
        public int Carbohydrates { get; set; }

        [Column("fats")]
        public int Fats { get; set; }

        [Column("photo_link")]
        public string PhotoLink { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("preparation_time")]
        public double PreparationTime { get; set; }


        public Meal(long userId, string name, string ingredients, int calories, string photoLink, string description, int proteins, int fats, int carbohydrates, double preparationTime)
        {
            UserId = userId;
            Name = name;
            Ingredients = ingredients;
            Calories = calories;
            PhotoLink = photoLink;
            Description = description;
            Proteins = proteins;
            Carbohydrates = carbohydrates;
            Fats = fats;
            PreparationTime = preparationTime;
        }
    }
}