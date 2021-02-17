using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SatApp.Views;
using Xamarin.Forms;

namespace SatApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppCenter.Start("bcc4cb1e-975a-4379-bcd5-1756e9ce4e24", typeof(Analytics), typeof(Crashes));


            MainPage = new Principal();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
