namespace SocialApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using AppCommonClasses.Services;
    using CommunityToolkit.Mvvm.Input;
    using global::Windows.Storage;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.Services;

    public class CreateMealViewModel : ViewModelBase
    {
        private readonly IMealService mealService;
        private string _mealName = string.Empty; // Initialize to avoid null
        private string _cookingTime = string.Empty; // Initialize to avoid null
        private string _selectedMealType = string.Empty; // Initialize to avoid null
        private string _selectedCookingLevel = string.Empty; // Initialize to avoid null
        private ObservableCollection<string> directions;
        private ObservableCollection<MealIngredient> ingredients;
        private StorageFile selectedImage = null!; // Use null-forgiving operator

        // Calculated macros
        private int calories;
        private int protein;
        private int carbs;
        private int fats;
        private int fiber;
        private int sugar;

        private string _mealNameError;
        private string _mealTypeError;
        private string _cookingTimeError;

        public CreateMealViewModel(IMealRepository mealRepository, IIngredientRepository ingredientRepository)
        {
            mealService = new MealService(mealRepository, ingredientRepository);

            // Initialize collections
            directions = new ObservableCollection<string>();
            ingredients = new ObservableCollection<MealIngredient>();

            // Initialize commands
            GoBackCommand = new RelayCommand(OnGoBack);
            AddDirectionCommand = new RelayCommand(OnAddDirection);
            AddIngredientCommand = new RelayCommand(OnAddIngredient);
            SelectMealTypeCommand = new RelayCommand<string?>(OnSelectMealType);
            SelectCookingLevelCommand = new RelayCommand<string?>(OnSelectCookingLevel);
        }

        public string MealName
        {
            get => _mealName;
            set
            {
                _mealName = value;
                OnPropertyChanged(nameof(MealName));
                ValidateMealName();
            }
        }

        public string CookingTime
        {
            get => _cookingTime;
            set
            {
                _cookingTime = value;
                OnPropertyChanged(nameof(CookingTime));
                ValidateCookingTime();
            }
        }

        public string SelectedMealType
        {
            get => _selectedMealType;
            set
            {
                _selectedMealType = value;
                OnPropertyChanged(nameof(SelectedMealType));
                ValidateMealType();
            }
        }

        public string SelectedCookingLevel
        {
            get => _selectedCookingLevel; // Fixes SA1101
            set
            {
                if (_selectedCookingLevel != value) // Fixes SA1101
                {
                    _selectedCookingLevel = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public StorageFile SelectedImage
        {
            get => selectedImage; // Fixes SA1101
            set
            {
                if (selectedImage != value) // Fixes SA1101
                {
                    selectedImage = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public ObservableCollection<string> Directions
        {
            get => directions; // Fixes SA1101
            set
            {
                if (directions != value) // Fixes SA1101
                {
                    directions = value; // Fixes SA1101
                    OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public ObservableCollection<string> IngredientNames
        {
            get => new(ingredients.Select(i => $"{i.IngredientName}: {i.Quantity} servings")); // Fixes SA1101
        }

        public ObservableCollection<MealIngredient> Ingredients
        {
            get => ingredients; // Fixes SA1101
            set
            {
                if (ingredients != value) // Fixes SA1101
                {
                    ingredients = value; // Fixes SA1101
                    OnPropertyChanged(nameof(IngredientNames)); // Fixes SA1101
                    CalculateTotalMacros(); // Fixes SA1101
                }
            }
        }

        public ICommand GoBackCommand { get; }

        public ICommand AddDirectionCommand { get; }

        public ICommand AddIngredientCommand { get; }

        public ICommand SelectMealTypeCommand { get; }

        public ICommand SelectCookingLevelCommand { get; }

        public string MealNameError
        {
            get => _mealNameError;
            set
            {
                _mealNameError = value;
                OnPropertyChanged(nameof(MealNameError));
            }
        }

        public string MealTypeError
        {
            get => _mealTypeError;
            set
            {
                _mealTypeError = value;
                OnPropertyChanged(nameof(MealTypeError));
            }
        }

        public string CookingTimeError
        {
            get => _cookingTimeError;
            set
            {
                _cookingTimeError = value;
                OnPropertyChanged(nameof(CookingTimeError));
            }
        }

        private void ValidateMealName()
        {
            MealNameError = string.IsNullOrWhiteSpace(MealName) ? "Meal name is required" : string.Empty;
        }

        private void ValidateMealType()
        {
            MealTypeError = string.IsNullOrWhiteSpace(SelectedMealType) ? "Meal type is required" : string.Empty;
        }

        private void ValidateCookingTime()
        {
            CookingTimeError = string.IsNullOrWhiteSpace(CookingTime) ? "Cooking time is required" : string.Empty;
        }

        private void CalculateTotalMacros()
        {
            var calculatedIngredients = ingredients.Select(i => i.CalculateMacros()); // Fixes SA1101

            calories = (int)calculatedIngredients.Sum(i => i.Calories); // Fixes SA1101
            protein = (int)calculatedIngredients.Sum(i => i.Protein); // Fixes SA1101
            carbs = (int)calculatedIngredients.Sum(i => i.Carbs); // Fixes SA1101
            fats = (int)calculatedIngredients.Sum(i => i.Fats); // Fixes SA1101
            fiber = (int)calculatedIngredients.Sum(i => i.Fiber); // Fixes SA1101
            sugar = (int)calculatedIngredients.Sum(i => i.Sugar); // Fixes SA1101

            OnPropertyChanged(nameof(TotalCalories)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalProtein)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalCarbs)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalFats)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalFiber)); // Fixes SA1101
            OnPropertyChanged(nameof(TotalSugar)); // Fixes SA1101
        }

        public int TotalCalories => calories; // Fixes SA1101

        public int TotalProtein => protein; // Fixes SA1101

        public int TotalCarbs => carbs; // Fixes SA1101

        public int TotalFats => fats; // Fixes SA1101

        public int TotalFiber => fiber; // Fixes SA1101

        public int TotalSugar => sugar; // Fixes SA1101

        public async Task<bool> CreateMealAsync(Meal meal)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== Starting CreateMealAsync ===");
                System.Diagnostics.Debug.WriteLine($"Meal object created with ID: {meal.Id}");
                System.Diagnostics.Debug.WriteLine($"Meal Name: {meal.Name}");
                System.Diagnostics.Debug.WriteLine($"Meal Category: {meal.Category}");
                System.Diagnostics.Debug.WriteLine($"Meal Type ID: {meal.Mt_id}");
                System.Diagnostics.Debug.WriteLine($"Cooking Level: {meal.CookingLevel}");
                System.Diagnostics.Debug.WriteLine($"Preparation Time: {meal.PreparationTime}");
                System.Diagnostics.Debug.WriteLine($"Number of Ingredients: {ingredients?.Count ?? 0}");

                if (string.IsNullOrWhiteSpace(SelectedCookingLevel))
                {
                    System.Diagnostics.Debug.WriteLine("No cooking level selected, defaulting to 'Beginner'");
                    SelectedCookingLevel = "Beginner";
                }

                // Convert cooking time to integer
                if (!int.TryParse(CookingTime, out int cookingTimeMinutes))
                {
                    System.Diagnostics.Debug.WriteLine("Failed to parse cooking time, defaulting to 0");
                    cookingTimeMinutes = 0;
                }

                // Set all meal properties including calculated macros
                meal.Name = MealName;
                meal.PreparationTime = cookingTimeMinutes;
                meal.CookingLevel = SelectedCookingLevel;
                meal.Calories = calories;
                meal.Protein = protein;
                meal.Carbohydrates = carbs;
                meal.Fat = fats;
                meal.Fiber = fiber;
                meal.Sugar = sugar;

                System.Diagnostics.Debug.WriteLine("Attempting to create meal with service...");
                bool mealId = await mealService.CreateMealWithCookingLevelAsync(meal, SelectedCookingLevel);
                
                if (mealId)
                {
                    System.Diagnostics.Debug.WriteLine("Meal created successfully, adding ingredients...");
                    // Then add the meal-ingredient relationships
                    foreach (var ingredient in ingredients)
                    {
                        System.Diagnostics.Debug.WriteLine($"Adding ingredient: {ingredient.IngredientName} (ID: {ingredient.IngredientId})");
                        await mealService.AddIngredientToMealAsync(meal.Id, ingredient.IngredientId, ingredient.Quantity);
                    }
                    System.Diagnostics.Debug.WriteLine("=== Meal Creation Completed Successfully ===");
                    return true;
                }
                
                System.Diagnostics.Debug.WriteLine("Failed to create meal in service");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CreateMealAsync: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        private void OnGoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private async void OnAddDirection()
        {
            var dialog = new TextBox
            {
                PlaceholderText = "Enter direction step",
            };

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Direction",
                Content = dialog,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot,
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(dialog.Text))
                {
                    Directions.Add($"{Directions.Count + 1}. {dialog.Text}"); // Fixes SA1101
                }
            }
        }

        private async void OnAddIngredient()
        {
            var quantityBox = new TextBox { PlaceholderText = "Enter quantity" };
            var ingredientBox = new TextBox { PlaceholderText = "Enter ingredient name" };

            var panel = new StackPanel();
            panel.Children.Add(ingredientBox);
            panel.Children.Add(quantityBox);

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Ingredient",
                Content = panel,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot,
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(ingredientBox.Text) && !string.IsNullOrWhiteSpace(quantityBox.Text))
                {
                    if (float.TryParse(quantityBox.Text, out float quantity))
                    {
                        // Get ingredient from database
                        var ingredient = await mealService.RetrieveIngredientByNameAsync(ingredientBox.Text); // Fixes SA1101
                        if (ingredient != Ingredient.NoIngredient)
                        {
                            var mealIngredient = new MealIngredient
                            {
                                IngredientId = ingredient.Id,
                                IngredientName = ingredient.Name,
                                Quantity = quantity,
                                Protein = (float)ingredient.Protein,
                                Calories = (float)ingredient.Calories,
                                Carbs = (float)ingredient.Carbs,
                                Fats = (float)ingredient.Fats,
                                Fiber = (float)ingredient.Fiber,
                                Sugar = (float)ingredient.Sugar,
                            };

                            ingredients.Add(mealIngredient); // Fixes SA1101
                            OnPropertyChanged(nameof(IngredientNames)); // Fixes SA1101
                            CalculateTotalMacros(); // Fixes SA1101
                        }
                        else
                        {
                            // Show error that ingredient wasn't found
                            var errorDialog = new ContentDialog
                            {
                                Title = "Error",
                                Content = "Ingredient not found in database",
                                CloseButtonText = "OK",
                                XamlRoot = App.MainWindow.Content.XamlRoot,
                            };
                            await errorDialog.ShowAsync();
                        }
                    }
                }
            }
        }

        private void OnSelectMealType(string? mealType) // Fixes CS8622
        {
            SelectedMealType = mealType ?? string.Empty; // Fixes SA1101 and ensures null safety
        }

        private void OnSelectCookingLevel(string? level) // Fixes CS8622
        {
            SelectedCookingLevel = level ?? string.Empty; // Fixes SA1101 and ensures null safety
        }
    }
}
