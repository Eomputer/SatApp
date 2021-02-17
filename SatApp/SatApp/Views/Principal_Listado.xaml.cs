using SatApp.Clases;
using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using SatApp.VistasModelos;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal_Listado : ContentPage
    {
        public Principal_Listado()
        {
            InitializeComponent();
            listado.RefreshCommand = new Command(() =>
            {
                ActualizarLista();
                listado.IsRefreshing = false;
            });
        }

        //Este evento se ejecuta cuando clickeas un parte.
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Variables.DatosParte = null;
                if (e.Item == null)
                    return;
                var item = (DatosParte)e.Item;
                Variables.DatosParte = item;

                RepositorySatApp sat = new RepositorySatApp();
                var todo = sat.GetSat(item.N_Parte);
                //var hora = todo.HoraInicioTarea.ToShortTimeString();

                //if (hora == "22:00" || hora == "23:00" || hora == "0:00")
                //{
                //    todo.HoraInicioTarea = DateTime.Now;
                //    sat.Guardar<SAT>(todo);
                //}

                sat.CerrarConexion();

                await Navigation.PushAsync(new PrincipalContentPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /*
         * Busca toda la información de los partes que se han trabajado y los envia a central.
         * Por cada parte genera 3 JSON.
         * Tras confirmar la transferencia y guardado de la información en central borra los partes enviados.
         */
        public async void EnvioMasivo(object sender, EventArgs e)
        {
            Login comprueba = new Login();

            if (comprueba.CompruebaTerminados())
            {
                Conecta envio = new Conecta();

                List<SAT> sat;
                List<SAT_Lineas> lineas = new List<SAT_Lineas>();


                RepositorySatApp bbdd = new RepositorySatApp();

                if (Variables.RevisaParte == true)
                {
                    sat = bbdd.SatRevisar<SAT>();
                }
                else
                    sat = bbdd.SatCerrado<SAT>();

                bool SAT = false;
                bool LINEAS = false;
                bool NOLINEAS = false;

                int numparte = 0;
                int part = 0;
                foreach (SAT parte in sat)
                {
                    sat[part].Hora = sat[part].Hora;
                    sat[part].HoraInicioTarea = sat[part].HoraInicioTarea;
                    sat[part].HoraFinTarea = sat[part].HoraFinTarea;

                    //En el caso de alguno de los Pendientes de las opciones es true. Esta a prueba
                    if (sat[part].PendienteRepuesto == true || sat[part].PendientePresupuesto == true || sat[part].PendienteRecambio == true || sat[part].SinReparar == true)
                    {
                        sat[part].Revisar = false;
                        sat[part].FechaEnvioApp = "1900-12-30 00:00:00.000";
                    }
                    else
                    {
                        sat[part].FechaEnvioApp = "1900-12-30 00:00:00.000";
                    }

                    lineas = bbdd.LineasSatCerrado<SAT_Lineas>(parte.N_Parte);

                    if (lineas.Count == 0)
                    {
                        await DisplayAlert("Aviso", "El parte número " + parte.N_Parte + " no tiene articulos ni servicios asociados por lo que no se pueden enviar los partes cerrados", "Ok");
                        NOLINEAS = true;
                    }
                    else
                    {
                        //Estamos en mantenimientos 03/02/2021
                        string[] rutaUrl = new string[] { "api/ListarSatLineas2", "api/SatAppRespaldo" };


                        for (int i = 0; i < rutaUrl.Length; i++)
                        {
                            if (rutaUrl[i] == "api/ListarSatLineas2")
                            {

                                if (await envio.UpdateDatosAsync(rutaUrl[i], lineas))
                                {
                                    LINEAS = true;
                                }
                                else
                                {
                                    LINEAS = false;
                                }
                            }
                            else if (rutaUrl[i] == "api/SatAppRespaldo")
                            {
                                if (LINEAS == true)
                                {
                                    if (await envio.UpdateDatosAsync(rutaUrl[i], sat[part]))
                                    {
                                        part++;
                                        SAT = true;
                                    }
                                    else
                                    {
                                        part++;
                                        SAT = false;
                                    }
                                }

                            }
                        }
                        bbdd.BorrarSatLineas2<SAT_Lineas>(parte.N_Parte);
                    }
                    if (SAT == false || LINEAS == false)
                    {
                        break;
                    }
                }
                if (SAT == true & LINEAS == true & NOLINEAS == false)
                {
                    bbdd.BorrarSatCerrado(sat);
                    bbdd.BorrarLineasSatCerrado(lineas);
                    await DisplayAlert("Aviso", "Enviados correctamente todos los partes trabajados", "Ok");
                    ActualizarLista();

                }
                else if (SAT == false & LINEAS == false & NOLINEAS == false)
                {
                    await DisplayAlert("Aviso", "Error en el envio de parte, el parte numero " + numparte + " no se envio correctamente", "Ok");
                }
                bbdd.CerrarConexion();
            }
            else
                await DisplayAlert("Aviso", "No hay partes cerrados que enviar", "Ok");
        }


        // Este metodo permite actualizar la lista de partes
        public void ActualizarLista()
        {
            try
            {
                RepositorySatApp bbdd = new RepositorySatApp();
                //BindingContext: propiedad del objeto de destino debe establecerse en el objeto de origen, se debe llamar al método 
                //(que se usa a menudo junto con la Binding en el objeto de destino para enlazar una propiedad de ese objeto a una propiedad del objeto de origen.
                //
                var vm = BindingContext as SAT_VM;
                vm.ListadoPartes.Clear();

                DatosParte parte;

                //Lista los partes asociados al Técnico. Busca por código de Personal
                List<SAT> ListadoSAT = bbdd.GetPartesAbiertos<SAT>(Variables.CodigoPersonal);

                //Añade los partes al ListView
                foreach (var st in ListadoSAT)
                {
                    parte = bbdd.CargarDatosDeParte(st);

                    vm.ListadoPartes.Add(parte);
                }
                bbdd.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}