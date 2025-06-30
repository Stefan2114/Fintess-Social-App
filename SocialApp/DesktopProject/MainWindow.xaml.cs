using System;
using System.Diagnostics;
using DesktopProject.Pages;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DesktopProject
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                this.InitializeComponent();
                /*
                Debug.WriteLine("MainWindow initialized successfully.");
                NavigationService.Instance.Initialize(MainFrame);
                Debug.WriteLine("NavigationService initialized with MainFrame.");
                MainFrame.Navigate(typeof(Pages.UserPage));
                Debug.WriteLine("Navigated to GoalPage.");
                */
                this.MainFrame.Navigate(typeof(UserPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MainWindow constructor: {ex.Message}");
                throw;
            }
        }
    }
}
