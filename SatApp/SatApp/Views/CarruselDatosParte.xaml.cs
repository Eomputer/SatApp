using Plugin.Messaging;
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
    public partial class CarruselDatosParte : ContentPage
    {
        public CarruselDatosParte()
        {
            InitializeComponent();

               if (Variables.BloqueaParte)
               {
                   RepositorySatApp BBDD = new RepositorySatApp();

                   var sati = BBDD.GetSat(Variables.DatosParte.N_Parte);

                   if (Variables.RevisaParte)
                   {
                       if ((Variables.DatosParte.Revisar == true) && (Variables.DatosParte.Realizado == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                       {
                           imgTelefono.IsEnabled = false;
                       }
                   }
                   else if (!Variables.RevisaParte)
                   {
                       if ((Variables.DatosParte.Realizado == true) && (Variables.DatosParte.Revisar == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                       {
                           imgTelefono.IsEnabled = false;
                       }
                   }
                   BBDD.CerrarConexion();
               }
           }

           //Método para llamar al cliente.Abre la pantalla del teléfono de llamadas
           public async void Llamar(object objeto, EventArgs e)
           {

               if (!string.IsNullOrEmpty(Telefono.Text))
               {
                   var llama = CrossMessaging.Current.PhoneDialer;

                   if (llama.CanMakePhoneCall)
                   {
                       llama.MakePhoneCall(Telefono.Text);
                   }
               }
               else
                   await Application.Current.MainPage.DisplayAlert("Aviso", "!!! El cliente no tiene teléfono !!!", "OK");
           }

           //Busca si tiene más de una dirección del cliente.
           public async void BuscarDireccion(object objeto, EventArgs e)
           {
               RepositorySatApp database = new RepositorySatApp();
               var direcciones = database.GetDireccionesCliente(Variables.DatosParte.CodigoCliente);
               database.CerrarConexion();

               if (direcciones.Count > 0)
               {
                   try
                   {
                       PopupDireccionesCliente popupDireccionesCliente = new PopupDireccionesCliente(direcciones);
                       popupDireccionesCliente.CallbackEvent += OnPopupUnitsCallback;
                       await PopupNavigation.Instance.PushAsync(popupDireccionesCliente);

                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
               }
               else
                   await Application.Current.MainPage.DisplayAlert("Aviso", "!!! No tiene más direcciones !!!", "OK");
           }

           public void OnPopupUnitsCallback(object sender, EventArgs e)
           {
               var x = sender as PopupDireccionesCliente;
               var direccion = x.direccion;
               if (direccion != null)
               {
                   RepositorySatApp database = new RepositorySatApp();
                   var parte = database.Get<SAT>(Variables.DatosParte.N_Parte);
                   database.CerrarConexion();
                   parte.Direccion = direccion.ID;

                   database.Guardar(parte);

                   Variables.DatosParte.DireccionParte = direccion.Direccion;
                   Variables.DatosParte.Poblacion = direccion.Poblacion;
                   Variables.DatosParte.Provincia = direccion.Provincia;
                   Variables.DatosParte.CodigoPostal = direccion.CP;

                   Variables.DatosParte.TelefonoCliente = direccion.Telefono;
               }
               else
               {
                   Application.Current.MainPage.DisplayAlert("Aviso", "!!! Dirección incorrecta !!!", "OK");
               }
           }
        }

    }
