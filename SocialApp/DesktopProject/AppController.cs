
namespace DesktopProject
{
    using System;
    using DesktopProject.Proxies;
    using ServerLibraryProject.Models;

    public sealed class AppController
    {
        private static readonly Lazy<AppController> instance = new(() => new AppController());

        public static AppController Instance => instance.Value;

        public User? CurrentUser { get; set; }

        public AppController() { }
    }
}
