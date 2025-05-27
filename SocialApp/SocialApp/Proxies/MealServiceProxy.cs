using AppCommonClasses.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppCommonClasses.Interfaces;
using System.Net;
using System;

namespace SocialApp.Proxies
{
    public class MealServiceProxy : IMealService
    {
        private readonly HttpClient _httpClient;

        public MealServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== CreateMealWithCookingLevelAsync Started ===");
                System.Diagnostics.Debug.WriteLine($"Sending meal creation request: {mealToCreate.Name}");
                System.Diagnostics.Debug.WriteLine($"Cooking Level: {cookingLevelDescription}");
                System.Diagnostics.Debug.WriteLine($"Base URL: {_httpClient.BaseAddress}");
                
                var url = $"meals/create-with-level?cookingLevelDescription={cookingLevelDescription}";
                System.Diagnostics.Debug.WriteLine($"Full URL: {url}");
                
                var response = await _httpClient.PostAsJsonAsync(url, mealToCreate);
                System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<dynamic>();
                    System.Diagnostics.Debug.WriteLine($"Meal creation response: {result}");
                    System.Diagnostics.Debug.WriteLine("=== CreateMealWithCookingLevelAsync Completed Successfully ===");
                    return true;
                }

                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Failed to create meal. Status: {response.StatusCode}, Error: {error}");
                System.Diagnostics.Debug.WriteLine("=== CreateMealWithCookingLevelAsync Failed ===");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in CreateMealWithCookingLevelAsync: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine("=== CreateMealWithCookingLevelAsync Failed with Exception ===");
                return false;
            }
        }

        public async Task<List<Meal>> RetrieveAllMealsAsync()
        {
            var response = await _httpClient.GetAsync("meals");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Meal>>() ?? new List<Meal>();

            throw new HttpRequestException($"Failed to retrieve meals. Status: {response.StatusCode}");
        }

        public async Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName)
        {
            var response = await _httpClient.GetAsync($"meals/ingredient/{ingredientName}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Ingredient>();

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            throw new HttpRequestException($"Failed to retrieve ingredient. Status: {response.StatusCode}");
        }

        public async Task<int> CreateMealAsync(Meal mealToCreate)
        {
            var response = await _httpClient.PostAsJsonAsync("meals", mealToCreate);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<int>();

            throw new HttpRequestException($"Failed to create meal. Status: {response.StatusCode}");
        }

        public async Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "meals/addingredient",
                new { mealIdentifier, ingredientIdentifier, ingredientQuantity });

            if (response.IsSuccessStatusCode)
                return true;

            throw new HttpRequestException($"Failed to add ingredient to meal. Status: {response.StatusCode}");
        }
    }
}
