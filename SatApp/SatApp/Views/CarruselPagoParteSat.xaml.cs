using Rg.Plugins.Popup.Services;
using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarruselPagoParteSat : ContentPage
    {
        public CarruselPagoParteSat()
        {
            InitializeComponent();

            //    RepositorySatApp database = new RepositorySatApp();

            //    Funciones funciones = new Funciones();

            //    var formaPago = database.ObtenerFormaPago(Variables.DatosParte.FormaPago);
            //    if (formaPago != null)
            //        txtFormaPago.Text = formaPago.Descripcion;

            //    if (Variables.BloqueaParte)
            //    {
            //        if (Variables.RevisaParte)
            //        {
            //            if ((Variables.DatosParte.Revisar == true) && (Variables.DatosParte.Realizado == false) && Variables.DatosParte.FechaEnvioApp == "1900-12-30 00:00:00.000")
            //            {
            //                txtFormaPago.IsEnabled = false;
            //                ImgFormaPago.IsEnabled = false;
            //            }

            //        }
            //        else if (!Variables.RevisaParte)
            //        {
            //            if ((Variables.DatosParte.Realizado == true) && (Variables.DatosParte.Revisar == false) && Variables.DatosParte.FechaEnvioApp == "1900-12-30 00:00:00.000")
            //            {
            //                txtFormaPago.IsEnabled = false;
            //                ImgFormaPago.IsEnabled = false;
            //            }
            //        }
            //    }
            //    funciones.CalcularParte(Variables.DatosParte.N_Parte);
            //    database.CerrarConexion();
         }

            public void OnPopupUnitsCallback(object sender, EventArgs e)
            {
                try
                {
                    var x = sender as PopupVarios;
                    var formaPago = x.forma_Pago;
                    RepositorySatApp database = new RepositorySatApp();

                    if (formaPago != null)
                    {
                        txtFormaPago.Text = formaPago.Descripcion;
                        var sat = database.Get<SAT>(Variables.DatosParte.N_Parte);
                        sat.Forma_Pago = formaPago.Codigo;
                        database.Guardar(sat);

                        Variables.DatosParte.FormaPago = sat.Forma_Pago;
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Aviso", "Forma de Pago incorrecta.", "OK");
                    }
                    database.CerrarConexion();
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Aviso ", ex.Message, " OK");
                }

            }

            async void BuscarFormaPago_Tapped(object sender, EventArgs e)
        {
            //Llama al popup cuando oprime el boton de la lupa
            try
            {
                var popup = new PopupVarios("FORMA_PAGO");
                popup.CallbackEvent += OnPopupUnitsCallback;
                await PopupNavigation.Instance.PushAsync(popup);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}