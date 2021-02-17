using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarruselSolucionParteSat : ContentPage
    {
        public CarruselSolucionParteSat()
        {
            InitializeComponent();

            if (Variables.BloqueaParte)
            {
                RepositorySatApp BBDD = new RepositorySatApp();

                var sati = BBDD.GetSat(Variables.DatosParte.N_Parte);
                BBDD.CerrarConexion();

                if (Variables.RevisaParte)
                {
                    if ((Variables.DatosParte.Revisar == true) && (Variables.DatosParte.Realizado == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                    {
                        entrySolucion.IsEnabled = false;
                        entryObservaciones.IsEnabled = false;
                    }

                }
                else if (!Variables.RevisaParte)
                {
                    if ((Variables.DatosParte.Realizado == true) && (Variables.DatosParte.Revisar == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                    {
                        entrySolucion.IsEnabled = false;
                        entryObservaciones.IsEnabled = false;
                    }
                }
            }
        }

        private void Guardar(object sender, EventArgs e)
        {
            try
            {
                RepositorySatApp database = new RepositorySatApp();

                SAT sat = new SAT();

                sat = database.Get<SAT>(Variables.DatosParte.N_Parte);

                sat.Solucion = entrySolucion.Text;
                sat.Observaciones = entryObservaciones.Text;
                database.Guardar(sat);
                database.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}