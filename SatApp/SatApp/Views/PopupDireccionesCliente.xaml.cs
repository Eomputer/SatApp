using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SatApp.Modelos;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDireccionesCliente : PopupPage
    {
        public Direccion_Cliente direccion = new Direccion_Cliente();
        public PopupDireccionesCliente(IEnumerable<Direccion_Cliente> listaDirecciones)
        {
            InitializeComponent();
            listadoDirecciones.ItemsSource = listaDirecciones;
        }

        public event EventHandler CallbackEvent;

        private void InvokeCallback()
        {
            CallbackEvent?.Invoke(this, EventArgs.Empty);
        }

        async void Tap_DireccionSeleccionada(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                direccion = (Direccion_Cliente)e.Item;
            }
            InvokeCallback();
            await PopupNavigation.Instance.PopAsync();
        }
    }
}