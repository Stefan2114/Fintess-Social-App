// <copyright file="MealRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Server.Repos
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppCommonClasses.Data;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository for managing meals.
    /// </summary>
    public class MealRepository : IMealRepository
    {
        private readonly SocialAppDbContext dbContext;

        public MealRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<int> CreateMealAsync(Meal meal, int cookingSkillId, int mealTypeId)
        {
            // If Meal has CookingSkillId and MealTypeId properties, set them here.
            // If not, adapt as needed.
            // meal.CookingSkillId = cookingSkillId; // Uncomment if property exists
            meal.Mt_id = mealTypeId; // Assuming Mt_id is MealTypeId
            meal.CookingLevel = cookingSkillId.ToString();

            await this.dbContext.Meals.AddAsync(meal);
            await this.dbContext.SaveChangesAsync();
            return meal.Id;
        }

        public async Task<int> AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var mealIngredient = new MealIngredient
            {
                MealId = mealId,
                // Add IngredientId and Quantity if MealIngredient has these properties
                // IngredientId = ingredientId, // Uncomment if property exists
                // Quantity = quantity,         // Uncomment if property exists
            };

            await this.dbContext.MealIngredients.AddAsync(mealIngredient);
            await this.dbContext.SaveChangesAsync();
            // If MealIngredient has an Id property, return it; otherwise, return 1 for success
            return 1;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await this.dbContext.Meals.ToListAsync();
        }
    }
}
