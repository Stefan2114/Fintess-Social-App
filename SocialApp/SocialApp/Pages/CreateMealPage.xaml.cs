namespace SocialApp.Pages
{
    using AppCommonClasses.Models;
    using global::Windows.Storage.Pickers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Media.Imaging;
    using SocialApp.ViewModels;
    using System;
    using WinRT.Interop;

    public sealed partial class CreateMealPage : Page
    {
        private readonly CreateMealViewModel viewModel;

        public CreateMealPage()
        {
            this.InitializeComponent();
            
            this.viewModel = App.Services.GetService<CreateMealViewModel>();
            this.DataContext = this.viewModel;
        }

        private async void ChoosePictureButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                this.viewModel.SelectedImage = file;
                var bitmapImage = new BitmapImage();
                /*
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    await bitmapImage.SetSourceAsync(stream);
                }*/

                this.SelectedImage.Source = bitmapImage;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== SaveButton_Click Started ===");
                
                // Clear previous validation messages
                MealNameError.Text = "";
                MealTypeError.Text = "";
                CookingTimeError.Text = "";
                
                bool hasErrors = false;

                // Validate required fields with visual feedback
                if (string.IsNullOrWhiteSpace(this.viewModel.MealName))
                {
                    MealNameError.Text = "Meal name is required";
                    hasErrors = true;
                }

                if (string.IsNullOrWhiteSpace(this.viewModel.SelectedMealType))
                {
                    MealTypeError.Text = "Meal type is required";
                    hasErrors = true;
                }

                if (string.IsNullOrWhiteSpace(this.viewModel.CookingTime))
                {
                    CookingTimeError.Text = "Cooking time is required";
                    hasErrors = true;
                }

                if (hasErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Validation failed - missing required fields");
                    return;
                }

                // Add debug logging
                System.Diagnostics.Debug.WriteLine("=== Starting Meal Creation ===");
                System.Diagnostics.Debug.WriteLine($"Meal Name: {this.viewModel.MealName}");
                System.Diagnostics.Debug.WriteLine($"Selected Meal Type: {this.viewModel.SelectedMealType}");
                System.Diagnostics.Debug.WriteLine($"Cooking Time: {this.viewModel.CookingTime}");
                System.Diagnostics.Debug.WriteLine($"Selected Cooking Level: {this.viewModel.SelectedCookingLevel}");
                System.Diagnostics.Debug.WriteLine($"Number of Directions: {this.viewModel.Directions?.Count ?? 0}");
                System.Diagnostics.Debug.WriteLine($"Number of Ingredients: {this.viewModel.Ingredients?.Count ?? 0}");
                System.Diagnostics.Debug.WriteLine($"Total Calories: {this.viewModel.TotalCalories}");
                System.Diagnostics.Debug.WriteLine($"Total Protein: {this.viewModel.TotalProtein}");
                System.Diagnostics.Debug.WriteLine($"Total Carbs: {this.viewModel.TotalCarbs}");
                System.Diagnostics.Debug.WriteLine($"Total Fats: {this.viewModel.TotalFats}");
                System.Diagnostics.Debug.WriteLine($"Total Fiber: {this.viewModel.TotalFiber}");
                System.Diagnostics.Debug.WriteLine($"Total Sugar: {this.viewModel.TotalSugar}");
                System.Diagnostics.Debug.WriteLine("===========================");

                System.Diagnostics.Debug.WriteLine("Creating meal object...");
                // Create meal with all required properties
                var meal = new Meal
                {
                    Name = this.viewModel.MealName,
                    Recipe = this.viewModel.Directions.Count > 0 ? string.Join("\n", viewModel.Directions) : "No directions provided",
                    Category = this.viewModel.SelectedMealType ?? "Other",
                    Mt_id = this.viewModel.SelectedMealType == "Breakfast" ? 1 :
                            this.viewModel.SelectedMealType == "Lunch" ? 2 :
                            this.viewModel.SelectedMealType == "Dinner" ? 3 : 4,
                    CookingLevel = this.viewModel.SelectedCookingLevel ?? "Beginner",
                    PreparationTime = int.TryParse(this.viewModel.CookingTime, out int time) ? time : 0,
                    PhotoLink = this.viewModel.SelectedImage?.Path ?? "default.jpg",
                    Calories = this.viewModel.TotalCalories,
                    Protein = this.viewModel.TotalProtein,
                    Carbohydrates = this.viewModel.TotalCarbs,
                    Fat = this.viewModel.TotalFats,
                    Fiber = this.viewModel.TotalFiber,
                    Sugar = this.viewModel.TotalSugar,
                    CreatedAt = DateTime.Now,
                    Ingredients = "",
                    ImagePath = this.viewModel.SelectedImage?.Path ?? "/images/default.jpg",
                    Image = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 },
                };

                System.Diagnostics.Debug.WriteLine("Calling CreateMealAsync...");
                bool success = await viewModel.CreateMealAsync(meal);
                System.Diagnostics.Debug.WriteLine($"CreateMealAsync result: {success}");

                if (success)
                {
                    System.Diagnostics.Debug.WriteLine("Meal created successfully, showing success dialog");
                    var dialog = new ContentDialog
                    {
                        Title = "Success",
                        Content = "Meal saved successfully!",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot,
                    };

                    await dialog.ShowAsync();
                    System.Diagnostics.Debug.WriteLine("Navigating to MainPage");
                    this.Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Meal creation failed, showing error dialog");
                    var dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Failed to save meal. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot,
                    };

                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SaveButton_Click: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"An error occurred: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot,
                };

                await dialog.ShowAsync();
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("=== SaveButton_Click Completed ===");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

