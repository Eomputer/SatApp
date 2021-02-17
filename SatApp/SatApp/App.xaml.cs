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


            //Clave para el AppCenter para diagnostico de error Log.
            AppCenter.Start("cb99de83-7323-4aa3-8338-2ea67a68bb42", typeof(Analytics), typeof(Crashes));


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
