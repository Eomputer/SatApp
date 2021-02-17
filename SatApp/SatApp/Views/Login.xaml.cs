using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SatApp.Clases.ListaEntidades;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        readonly Conecta Conexion = new Conecta();
        readonly RepositorySatApp datos = new RepositorySatApp();
        public Login()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (!Conexion.AccesoInternet())//Verifica la conexión a Internet
            {
                await DisplayAlert("Conexión", "No tienes conexión a Internet", "OK");
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(Usuario.Text) && (!string.IsNullOrEmpty(Pass.Text)))
                    {
                        Conecta userPass = new Conecta();

                        lblLoadingText.Text = "Autentificando usuario...";
                        popupLoadingView.IsVisible = true;
                        Variables.CodigoPersonal = Convert.ToInt32(Usuario.Text);
                        Variables.ClavePersonal = Pass.Text;
                        await logotipo.RotateTo(360, 5000);

                        if (CargarDatos.IsToggled == true)//Si el option esta activado. Entra
                        {
                            if (Variables.ServerUrl != null)//Si ServerUrl esta vacío es la primera vez esta recién instalada
                            {
                                    if (await userPass.Acredita(Convert.ToInt32(Usuario.Text), Pass.Text))//Verifica la autenticación
                                    {
                                        Variables.CodigoPersonal = Convert.ToInt32(Usuario.Text);
                                        Variables.ClavePersonal = Pass.Text;

                                        popupLoadingView.IsVisible = false;

                                        // Comprueba si hay partes cerrados pendientes de enviar. 
                                        if (CompruebaTerminados() == true)
                                        {
                                            await DisplayAlert("Aviso", "Hay partes cerrados pendientes de enviar. Envie los partes cerrados para poder descargar", "OK");
                                            await logotipo.RotateTo(360, 5000);
                                            popupLoadingView.IsVisible = true;
                                            await Navigation.PushModalAsync(new NavigationPage(new Principal_Listado()));
                                        }
                                        else
                                        {
                                            //Si las credenciales son correctas, todos los partes estan cerrados se procede a descarga completa o parcial.
                                            lblLoadingText.Text = "Descargando datos...";
                                            popupLoadingView.IsVisible = true;
                                            await logotipo.RotateTo(360, 5000);

                                            //Cada vez que se loguea borra las tablas siguientes
                                            //Excepto cuando haya partes pendientes de enviar
                                            var enviadoapp = PartesSinEnviar();

                                            if (enviadoapp == false)
                                            {
                                                datos.BorrarSat<SAT>();
                                                datos.BorrarSatLineas<SAT_Lineas>();
                                                datos.BorrarSatEquipo<SAT_Equipo>();
                                                datos.BorrarClientes<Cliente>();
                                                datos.BorrarClienteMaquinas<Cliente_Maquina>();
                                                datos.BorrarDireccionCliente<Direccion_Cliente>();
                                                datos.BorrarMaquinas<Maquinas>();
                                            }

                                            //Recuperamos la configuración para bloquear o no la modificación del parte cuando se cierre
                                            var configuraciones = datos.GetAll<Conexiones>();
                                            foreach (var b in configuraciones)
                                            {
                                                Variables.BloqueaParte = b.Bloquea_Parte;
                                            }
                                            // Procede a descargar las tablas.
                                            SincronizarDatos(Convert.ToInt32(Usuario.Text), Pass.Text);
                                        }
                                    }
                                    else
                                    {
                                        await DisplayAlert("Login", "!!! Las credenciales no son correctas !!!", "OK");
                                        lblLoadingText.Text = "";
                                    }                               
                            }
                            else
                            {
                                await DisplayAlert("Aviso", "! Si es la primera vez que inicia sesión, configure servidor y puerto !", "OK");
                            }

                        }
                        else //Si el boton option esta desactivado. También Verifica la autenticación 
                        {
                            if (Variables.ServerUrl != null) //Si ServerUrl esta vacío es la primera vez esta recién instalada
                            {
                                    if (await userPass.Acredita(Convert.ToInt32(Usuario.Text), Pass.Text))
                                    {
                                        var configuraciones = datos.GetAll<Conexiones>();

                                        foreach (var b in configuraciones)
                                        {
                                            Variables.BloqueaParte = b.Bloquea_Parte;
                                        }


                                        var valores = datos.GetAll<Valores_SAT>().FirstOrDefault();

                                        if(valores != null) //Mientras no hayan datos en la DB se controla el error de null
                                        {
                                            Variables.ArticuloVarios = valores.SAT_ArticuloVarios;
                                            Variables.RevisaParte = valores.SAT_RevisarParteAPP;
                                            Variables.CodigoPersonal = Convert.ToInt32(Usuario.Text);
                                        }

                                        
                                        //Muestra la vista de los partes.
                                        lblLoadingText.Text = "";
                                        await Navigation.PushModalAsync(new NavigationPage(new Principal_Listado()));
                                    }
                                    else
                                    {
                                        await DisplayAlert("Login", "!!! Las credenciales no son correctas !!!", "OK");
                                        lblLoadingText.Text = "";
                                    }
                                }                                
                            else
                            {
                                await DisplayAlert("Aviso", "Si es la primera vez que inicia sesion debe descargar los partes", "OK");
                            }
                        }
                    }
                    else
                    {
                        lblLoadingText.IsVisible = true;
                        await DisplayAlert("Login", "!!! Introduzca un usuario y su contraseña !!!", "OK");
                        lblLoadingText.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        // Esta funcion permite enviar los partes cerrados al servidor
        public bool CompruebaTerminados()
        {
            bool resultado = false;

            List<SAT> sat;
            List<SAT_Lineas> lineas;
            List<SAT_Equipo> equipo;

            RepositorySatApp BBDD = new RepositorySatApp();

            var valores = BBDD.GetAll<Valores_SAT>().FirstOrDefault();

            if(valores != null)
            {
                Variables.ArticuloVarios = valores.SAT_ArticuloVarios;
                Variables.RevisaParte = valores.SAT_RevisarParteAPP;
            }                      

            if (Variables.RevisaParte == true)
            {
                sat = BBDD.SatRevisar<SAT>();
            }
            else
                sat = BBDD.SatCerrado<SAT>();

            if (sat.Count >= 1)
            {
                foreach (var parte in sat)
                {
                    lineas = BBDD.LineasSatCerrado<SAT_Lineas>(parte.N_Parte);
                    equipo = BBDD.EquipoSatCerrado<SAT_Equipo>(parte.Maquina);

                }
                resultado = true;
            }
            else
                resultado = false;

            return resultado;
        }

        //Mientras la tabla SAT este llena es que hay partes por enviar
        public bool PartesSinEnviar()
        {
            RepositorySatApp datos = new RepositorySatApp();
            var list = datos.PartesSinEnviar<SAT>();
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Descarga de Pirineos las siguientes tablas al respectivo técnico con su usuario y contraseña.
        public async void SincronizarDatos(int persona, string contraseña)
        {
            Conecta Conexion = new Conecta();

            try
            {
                //Lista las Entidades del Modelo
                List<string> Entidad = ((Entidades[])Enum.GetValues(typeof(Entidades))).Select(c => c.ToString()).ToList();

                var list = datos.GetListAticulos();
                string ruta = null;                

                foreach (var Enti in Entidad)
                {
                    Variables.Entidad = Enti;

                    ruta = "api/Descarga/" + Enti;
                    if (Variables.ErrorTransDatos != 1)
                    {
                        if (list.Count() == 0)
                        {
                            switch (Enti)
                            {
                                case "ListarGenerales":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Generales>(ruta); });
                                    break;
                                case "ListarFormaPago":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Forma_Pago>(ruta); });
                                    break;
                                case "ListarPersonal":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Personal>(ruta); });
                                    break;
                                case "ListarProvincias":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Provincias>(ruta); });
                                    break;
                                case "ListarPaises":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Pais>(ruta); });
                                    break;
                                case "ListarIva":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Iva>(ruta); });
                                    break;
                                case "ListarRegimenIva":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Regimen_IVA>(ruta); });
                                    break;
                                case "ListarValoresSat":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Valores_SAT>(ruta); });
                                    break;
                                case "ListarArticulos":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Articulos>(ruta); });
                                    break;
                                case "ListarServicios":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Servicios>(ruta); });
                                    break;
                                case "ListarClientes":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<Cliente>(persona, contraseña, ruta); });
                                    break;
                                case "ListarDireccionCliente":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<Direccion_Cliente>(persona, contraseña, ruta); });
                                    break;
                                case "ListarClienteMaquina":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<Cliente_Maquina>(persona, contraseña, ruta); });
                                    break;
                                case "ListarSatEquipo":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<SAT_Equipo>(ruta); });
                                    break;
                                case "ListarMaquinas":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Maquinas>(ruta); });
                                    break;
                                case "ListarSat":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<SAT>(persona, contraseña, ruta); });
                                    break;
                                case "ListarSatLineas":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<SAT_Lineas>(persona, contraseña, ruta); });
                                    break;
                                case "UpdateRecibirLineas":
                                    await Task.Run(async () => { await Conexion.PostDatosUpdateAsync<SAT>(ruta); });
                                    break;
                            }
                        }
                        else
                        {
                            switch (Enti)
                            {
                                case "ListarSat":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<SAT>(persona, contraseña, ruta); });
                                    break;
                                case "ListarSatLineas":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<SAT_Lineas>(persona, contraseña, ruta); });
                                    break;
                                case "ListarSatEquipo":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<SAT_Equipo>(ruta); });
                                    break;
                                case "ListarClientes":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<Cliente>(persona, contraseña, ruta); });
                                    break;
                                case "ListarClienteMaquina":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<Cliente_Maquina>(persona, contraseña, ruta); });
                                    break;
                                case "ListarDireccionCliente":
                                    await Task.Run(async () => { await Conexion.PostDatosAsync<Direccion_Cliente>(persona, contraseña, ruta); });
                                    break;
                                case "ListarMaquinas":
                                    await Task.Run(async () => { await Conexion.GetDatosAsync<Maquinas>(ruta); });
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Variables.ErrorTransDatos = 0;
                        await DisplayAlert("Transmisión", "Error en las comunicaciones", "OK");
                    }
                }
                popupLoadingView.IsVisible = false;
                await Navigation.PushModalAsync(new NavigationPage(new Principal_Listado()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}