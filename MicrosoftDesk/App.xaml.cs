using MicrosoftDesk.Services;
using MicrosoftHelpDesk.Models;
using MicrosoftHelpDesk.Views;
using Xamarin.Forms;

namespace MicrosoftDesk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RequestsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
