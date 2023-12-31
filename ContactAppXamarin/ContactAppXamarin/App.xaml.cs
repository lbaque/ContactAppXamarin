using ContactAppXamarin.Services;
using ContactAppXamarin.View;
using ContactAppXamarin.ViewModel;
using SixLabors.ImageSharp.Processing;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactAppXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Locator.Initialize();
            DependencyService.Register<IHttpClientUris, HttpClientUris>();
            DependencyService.Register<ITaskBackground, TaskBackground>();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            ITaskBackground taskBackground = DependencyService.Get<ITaskBackground>();
            taskBackground.Start();
        }

        

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
