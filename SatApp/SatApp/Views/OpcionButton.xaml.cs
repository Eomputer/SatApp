using SatApp.ServiciosDB;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpcionButton : ContentPage
    {
        public OpcionButton()
        {
            InitializeComponent();
        }

        //RadioButtonGroupView: En la Vista necesita el Nuget Xamarin.Forms.InputKit. Para que puedan funcionar los RadioButton por la última actualizacion de 4.8.0.1821
        //Ya que cambiaron Text por Content. De momento Tiramos por aqui. 17/02/2021       

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (OptRealizado.IsChecked == false && OptPendRepuesto.IsChecked == false && OptPendPresupuesto.IsChecked == false && OptPendRecambio.IsChecked == false && OptSinReparar.IsChecked == false)
                {
                    DisplayAlert("Opciones", "! Elija una opción antes de cerrar el parte !", "OK");
                }
                else
                {
                    //Asignará a true dependiendo de lo que haya marcado, esta variable irá a CierrarPareSat

                    if (OptRealizado.IsChecked)
                    {
                        Variables.DatosParte.Revisar = true;
                        Variables.DatosParte.Realizado = false;
                        Variables.OptPendienteRepuesto = false;
                        Variables.OptPendientePresupuesto = false;
                        Variables.OptPendienteRecambio = false;
                        Variables.OptSinReparar = false;

                        Variables.NoRealizado = false;
                    }

                    if (OptPendRepuesto.IsChecked)
                    {
                        Variables.OptPendienteRepuesto = true;
                        Variables.DatosParte.Realizado = false;
                        Variables.OptPendientePresupuesto = false;
                        Variables.OptPendienteRecambio = false;
                        Variables.OptSinReparar = false;

                        Variables.NoRealizado = true;
                    }
                    if (OptPendPresupuesto.IsChecked)
                    {
                        Variables.OptPendientePresupuesto = true;
                        Variables.DatosParte.Realizado = false;
                        Variables.OptPendienteRepuesto = false;
                        Variables.OptPendienteRecambio = false;
                        Variables.OptSinReparar = false;

                        Variables.NoRealizado = true;
                    }

                    if (OptPendRecambio.IsChecked)
                    {
                        Variables.OptPendienteRecambio = true;
                        Variables.DatosParte.Realizado = false;
                        Variables.OptPendientePresupuesto = false;
                        Variables.OptPendienteRepuesto = false;
                        Variables.OptSinReparar = false;

                        Variables.NoRealizado = true;
                    }

                    if (OptSinReparar.IsChecked)
                    {
                        Variables.OptSinReparar = true;
                        Variables.DatosParte.Realizado = false;
                        Variables.OptPendienteRepuesto = false;
                        Variables.OptPendientePresupuesto = false;
                        Variables.OptPendienteRecambio = false;

                        Variables.NoRealizado = true;
                    }

                    CarruselCerrarParteSat cerrar = new CarruselCerrarParteSat();
                    cerrar.FinalizarParte();

                    Navigation.InsertPageBefore(new Principal_Listado(), Navigation.NavigationStack[0]);
                    Navigation.PopToRootAsync(); //Envia de vuelta a la pagina princiaplW  
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (OptRealizado.IsChecked || OptPendRepuesto.IsChecked || OptPendRecambio.IsChecked || OptSinReparar.IsChecked)
                {
                    OptRealizado.IsChecked = false;
                    OptPendRepuesto.IsChecked = false;
                    OptPendRecambio.IsChecked = false;
                    OptSinReparar.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}