using Microsoft.AspNetCore.Mvc;
using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;

namespace MealSocialServerMVC.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        // GET: /Meal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Meal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal model)
        {
            System.Diagnostics.Debug.WriteLine("POST Create action called");

            // Handle image upload if provided
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(ms);
                    model.Image = ms.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                // Set CreatedAt to now
                model.CreatedAt = DateTime.Now;

                try
                {
                    bool created = await mealService.CreateMealWithCookingLevelAsync(model, model.CookingLevel);

                    if (created)
                        return RedirectToAction("Index");

                    System.Diagnostics.Debug.WriteLine("Meal creation failed in service/repository.");
                    ModelState.AddModelError("", "Failed to create meal.");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex}");
                    ModelState.AddModelError("", $"Exception: {ex.Message}");
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"ModelState error for {key}: {error.ErrorMessage}");
                    }
                }
            }
            return View(model);
        }

        // GET: /Meal/Index
        public async Task<IActionResult> Index()
        {
            var meals = await mealService.RetrieveAllMealsAsync();
            return View(meals);
        }
    }
}
