using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.IO;

namespace SocialApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : ControllerBase
    {
        private readonly MealServiceProxy mealServiceProxy;

        public MealController(MealServiceProxy mealServiceProxy)
        {
            this.mealServiceProxy = mealServiceProxy;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            var meals = await mealServiceProxy.RetrieveAllMealsAsync();
            return Ok(meals);
        }

        // POST: api/Meal
        [HttpPost]
        public async Task<ActionResult> CreateMeal([FromForm] Meal meal)
        {
            // Handle image upload if provided
            if (meal.ImageFile != null && meal.ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await meal.ImageFile.CopyToAsync(ms);
                    meal.Image = ms.ToArray();
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await mealServiceProxy.CreateMealWithCookingLevelAsync(meal, meal.CookingLevel ?? "Beginner");
                if (result)
                {
                    return CreatedAtAction(nameof(GetMeals), new { id = meal.Id }, meal);
                }
                return BadRequest("Failed to create meal.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating meal: {ex.Message}");
            }
        }
    }
}
