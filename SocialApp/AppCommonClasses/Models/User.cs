using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("iamge")]
        public string? Image { get; set; }

        [Required]
        [Column("height")]
        [Range(1, double.MaxValue, ErrorMessage = "Height must be a positive number.")]
        public double Height { get; set; }

        [Required]
        [Column("weight")]
        [Range(1, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public double Weight { get; set; }

        [Required]
        [Column("goal")]
        public string Goal { get; set; }


        //we need to calculate the total macros for a person by this formula:
        // basal_metabolism_rate = 10 * weight + 5 * height
        // if female: basal_metabolism_rate -= 200
        // total_daily_energy_expenditure = basal_metabolism_rate * activity_level_multyplier (1.2, 1.4, 1.6, 1,7, 1.9)
        // if goal == "lose weight": calories = total_daily_energy_expenditure - 500
        // if goal == "gain muscles": calories = total_daily_energy_expenditure + 500
        // if goal == "maintain": calories = total_daily_energy_expenditure
        // protein = 30%
        // carbohydrates = 40%
        // fat = 30%

        [Column("proteins")]
        public int Proteins { get; set; }

        [Column("carbohydrates")]
        public int Carbohydrates { get; set; }

        [Column("fats")]
        public int Fats { get; set; }

    }
}
