using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SatApp.Modelos;
using SatApp.ServiciosDB;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupArticulosServicios : PopupPage
    {

        private readonly bool EsArticulo;
        public Articulos articulo;
        public Servicios servicio;
        public string referenciaItem;

        public PopupArticulosServicios(IEnumerable<Articulos> listadoArticulos, bool tipoItem)
        {
            InitializeComponent();
            listaArticulosServicios.ItemsSource = listadoArticulos;
            EsArticulo = tipoItem;
        }

        public PopupArticulosServicios(IEnumerable<Servicios> listadoArticulos, bool tipoItem)
        {
            InitializeComponent();
            listaArticulosServicios.ItemsSource = listadoArticulos;
            EsArticulo = tipoItem;
        }


        public event EventHandler CallbackEvent;

        private void InvokeCallback()
        {
            CallbackEvent?.Invoke(this, EventArgs.Empty);
        }

        async void Tap_ArticuloServicioSeleccionado(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                if (EsArticulo)
                {
                    articulo = (Articulos)e.Item;
                    Variables.Referencia = articulo.Referencia.ToString();
                }
                else
                {
                    servicio = (Servicios)e.Item;
                    Variables.Referencia = servicio.Referencia.ToString();

                }
            }
            InvokeCallback();
            await PopupNavigation.Instance.PopAsync();
        }

    }
}