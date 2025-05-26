namespace AppCommonClasses.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ingredients")]
    public class Ingredient
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("calories")]
        public double Calories { get; set; }

        [Column("protein")]
        public double Protein { get; set; }

        [Column("carbohydrates")]
        public double Carbs { get; set; }

        [Column("fat")]
        public double Fats { get; set; }

        public Ingredient(int id, string name, double calories, double protein, double carbs, double fats, double fiber, double sugar)
        {
            Id = id;
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fats = fats;
        }

        public Ingredient()
        {
        }
    }
}