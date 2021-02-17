using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SatApp.Modelos;
using SatApp.Repository;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupVarios : PopupPage
    {
        public string titulo = "";
        public Iva iva;
        public Forma_Pago forma_Pago;
        public PopupVarios(string _titulo)
        {
            InitializeComponent();

            titulo = _titulo;

            RepositorySatApp database = new RepositorySatApp();

            if (titulo.Equals("IVA"))
                listado.ItemsSource = database.GetAll<Iva>();
            else if (titulo.Equals("FORMA_PAGO"))
                listado.ItemsSource = database.GetAll<Forma_Pago>();
            database.CerrarConexion();
        }

        public event EventHandler CallbackEvent;

        private void InvokeCallback()
        {
            //Una vez terminado el seleccionar limpia el popup
            try
            {
                CallbackEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        async void ElementoElegido(object sender, ItemTappedEventArgs e)
        {
            //Extrae el elemento seleccionado de la forma pago
            try
            {
                if (e.Item != null)
                {
                    if (titulo.Equals("IVA"))
                    {
                        iva = (Iva)e.Item;
                    }
                    else if (titulo.Equals("FORMA_PAGO"))
                    {
                        forma_Pago = (Forma_Pago)e.Item;
                    }
                }
                InvokeCallback();
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}