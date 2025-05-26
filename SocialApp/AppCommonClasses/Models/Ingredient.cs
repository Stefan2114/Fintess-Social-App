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
        public int Calories { get; set; }

        [Column("proteins")]
        public int Proteins { get; set; }

        [Column("carbohydrates")]
        public int Carbs { get; set; }

        [Column("fats")]
        public int Fats { get; set; }

        public Ingredient(int id, string name, int calories, int protein, int carbs, int fats)
        {
            Id = id;
            Name = name;
            Calories = calories;
            Proteins = protein;
            Carbs = carbs;
            Fats = fats;
        }

        public Ingredient()
        {
        }
    }
}