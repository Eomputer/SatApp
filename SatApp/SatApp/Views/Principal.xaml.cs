using SatApp.Repository;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : Xamarin.Forms.TabbedPage
    {
        readonly RepositorySatApp repo;
        public Principal()
        {
            InitializeComponent();

            //Crea la Base de Datos
            repo = new RepositorySatApp();
            repo.CrearBBDD();

            //Esto es para que el TabbedPage se coloque abajo. Por defecto lo pone arriba
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}