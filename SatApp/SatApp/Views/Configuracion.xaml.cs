using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage
    {
        private readonly IAdapter Adaptador;
        private readonly IBluetoothLE Bluetooth;
        readonly ObservableCollection<IDevice> Lista;
        IDevice dispositivo;
        bool BloqueParte = false;
        bool epson = false;

        // En el constructor se colocan los datos de servidor y puerto en caso de que existan, ademas de
        // la activación de algunos componentes

        public Configuracion()
        {
            InitializeComponent();

            RepositorySatApp DatosServidor = new RepositorySatApp();
            var Datos_Comunicacion = DatosServidor.GetAll<Conexiones>();

            if (Variables.ConfImpresora == true)
            {
                Servidor.IsEnabled = false;
                Puerto.IsEnabled = false;
                BtnSincroniza.IsEnabled = false;
                BtnConfiguracion.IsEnabled = false;
            }

            if (Datos_Comunicacion != null)//Cuando se instala la app por primera vez la tabla Conexiones no existe y devuelve.
            {
                if (Datos_Comunicacion.Count != 0)//Una vez que esta instalada la app ya existe la tabla conexiones, pero solo entrará cuando existan datos.
                {
                    Servidor.Text = Datos_Comunicacion[0].IP;
                    Puerto.Text = Datos_Comunicacion[0].Puerto;
                    BtnSincroniza.IsEnabled = true;
                    BtnEliminaBBDD.IsEnabled = true;
                }
            }

            NavigationPage.SetHasBackButton(this, false);

            //Bluetooth = CrossBluetoothLE.Current;
            //Adaptador = CrossBluetoothLE.Current.Adapter;


            Lista = new ObservableCollection<IDevice>();
            DispositivosLista.ItemsSource = Lista;
            DatosServidor.CerrarConexion();
        }

        #region Escaner dispositivos bluetooth

        //Realiza la busqueda de dispositivos bluetooth
        public async void BusquedaDispositivo(object objeto, EventArgs E)
        {
            try
            {
                if (Bluetooth.State == BluetoothState.Off)
                {
                    await DisplayAlert("Atención", "Bluetooth deshabilitado", "OK");
                }
                else
                {
                    //Activa mensaje de aviso de busqueda de dispositivos

                    //popupLoadingView.IsVisible = true;
                    //lblLoadingText.Text = "Buscando dispositivos...";
                    activityIndicator.IsRunning = true;

                    Lista.Clear();
                    Adaptador.ScanTimeout = 10000;
                    Adaptador.ScanMode = ScanMode.Balanced;

                    if (epson)
                    {
                        var pareados = Adaptador.GetSystemConnectedOrPairedDevices();
                        foreach (var g in pareados)
                        {
                            if (!Lista.Contains(g))
                                Lista.Add(g);

                        }
                    }

                    Adaptador.DeviceDiscovered += (obj, a) =>

                    {
                        if (!Lista.Contains(a.Device))
                            Lista.Add(a.Device);

                    };

                    if (!Bluetooth.Adapter.IsScanning)
                    {

                        await Adaptador.StartScanningForDevicesAsync();

                    }
                }
                //Desactiva mensaje de busqueda de dsipositivos
                popupLoadingView.IsVisible = false;
                activityIndicator.IsRunning = false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region conexión bluetooth

        //Conexión manual en caso de que el dispositivo de impresión no este configurado
        public async void ConexionDispositivo()
        {
            dispositivo = DispositivosLista.SelectedItem as IDevice;

            var respuesta = await DisplayAlert("Aviso", "¿Conectarse a este dispositivo?", "Conectar", "Cancelar");

            if (!respuesta)
                return;


            await Adaptador.StopScanningForDevicesAsync();

            try
            {

                popupLoadingView.IsVisible = true;
                lblLoadingText.Text = "Conectando al dispositivo...";
                activityIndicator.IsRunning = true;

                var parameters = new ConnectParameters(forceBleTransport: true);
                await Adaptador.ConnectToDeviceAsync(dispositivo, parameters);

                await DisplayAlert("Conectado:", "Dispositivo:" + dispositivo.Name + dispositivo.State, "OK");

                Lista.Clear();
                Lista.Add(dispositivo);

                popupLoadingView.IsVisible = false;
                activityIndicator.IsRunning = false;

            }
            catch (Exception ex)
            {
                popupLoadingView.IsVisible = false;
                activityIndicator.IsRunning = false;

                await DisplayAlert("Aviso", ex.Message, "OK");

                dispositivo = null;
            }

        }
        #endregion

        #region Botones selección impresora       
        //Boton que selecciona la impresora que  es epson. Valor variable epson = true

        public void ImEpson(object sender, ToggledEventArgs e)
        {
            bool valor = e.Value;
            if (valor == true)
            {
                epson = true;
            }
            else
            {
                epson = false;
            }
        }
        #endregion

        // Permite seleccionar el bloqueo de modificación de partes cuando este se cierra        
        public void BlParte(object sender, ToggledEventArgs e)
        {
            bool valor = e.Value;
            if (valor == true)
            {
                BloqueParte = true;
            }
            else
            {
                BloqueParte = false;

            }
        }

        //Verifica si existe Partes Abiertos
        public bool Haypartesabiertos()
        {
            try
            {
                RepositorySatApp datos = new RepositorySatApp();
                var list = datos.GetPartesAbiertos<SAT>(Variables.CodigoPersonal);
                datos.CerrarConexion();
                if (list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void BtnSincroniza_Clicked(object sender, EventArgs e)
        {
            //Conecta Conexion = new Conecta();

            //if (!Conexion.AccesoInternet())
            //{
            //    await DisplayAlert("Conexión", "No tienes conexión a Internet", "OK");
            //}
            //else
            //{
            //    if (Haypartesabiertos() == true)
            //    {
            //        await DisplayAlert("Aviso", "Hay partes abiertos, cierre los partes antes de actualizar la Base de Datos", "OK");
            //    }
            //    else
            //    {
            //        DispositivosLista.IsVisible = false;
            //        popupLoadingView.IsVisible = true;
            //        lblLoadingText.Text = "Descargando datos..";
            //        activityIndicator.IsRunning = true;

            //        //Lista las Entidades del Modelo
            //        List<string> Entidad = ((Entidades[])Enum.GetValues(typeof(Entidades))).Select(c => c.ToString()).ToList();

            //        string ruta = null;
            //        try
            //        {
            //            foreach (var Enti in Entidad)
            //            {
            //                ruta = "/api/DescargasCompletas/" + Enti;

            //                switch (Enti)
            //                {
            //                    case "ListarActividad":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Actividades>(ruta); });
            //                        break;
            //                    case "ListarArticulos":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Articulos>(ruta); });
            //                        break;
            //                    case "ListarClientes":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Cliente>(ruta); });
            //                        break;
            //                    case "ListarClienteMaquina":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Cliente_Maquina>(ruta); });
            //                        break;
            //                    case "ListarColores":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Colores>(ruta); });
            //                        break;
            //                    case "ListarComercialCliente":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<ComercialCliente>(ruta); });
            //                        break;
            //                    case "ListarDireccionCliente":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Direccion_Cliente>(ruta); });
            //                        break;
            //                    case "ListarFamilia":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Familias>(ruta); });
            //                        break;
            //                    case "ListarFamiliaServicios":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Familia_Servicios>(ruta); });
            //                        break;
            //                    case "ListarFormaPago":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Forma_Pago>(ruta); });
            //                        break;
            //                    case "ListarFotosArticulo":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<FotosArticulo>(ruta); });
            //                        break;
            //                    case "ListarGenerales":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Generales>(ruta); });
            //                        break;
            //                    case "ListarIva":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Iva>(ruta); });
            //                        break;
            //                    case "ListarRegimenIva":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Regimen_IVA>(ruta); });
            //                        break;
            //                    case "ListarGrupoCliente":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Grupo_Cliente>(ruta); });
            //                        break;
            //                    case "ListarGrupoArticulo":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Grupo_Articulo>(ruta); });
            //                        break;
            //                    case "ListarPais":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Pais>(ruta); });
            //                        break;
            //                    case "ListarLineasOfertas":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Lineas_Ofertas>(ruta); });
            //                        break;
            //                    case "ListarMaquinas":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Maquinas>(ruta); });
            //                        break;
            //                    case "ListarMarca":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Marca>(ruta); });
            //                        break;
            //                    case "ListarOfertas":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Ofertas>(ruta); });
            //                        break;
            //                    case "ListarServicios":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Servicios>(ruta); });
            //                        break;
            //                    case "ListarLineaPedCli":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Linea_Ped_Cli>(ruta); });
            //                        break;
            //                    case "ListarPedidoCliente":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Pedido_Cliente>(ruta); });
            //                        break;
            //                    case "ListarPrecioCliente":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<PrecioCliente>(ruta); });
            //                        break;
            //                    case "ListarPersonal":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Personal>(ruta); });
            //                        break;
            //                    case "ListarPropiedadesArticulo":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<PropiedadesArticulo>(ruta); });
            //                        break;
            //                    case "ListarProvincias":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Provincias>(ruta); });
            //                        break;
            //                    case "ListarSubFamilia":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Subfamilia>(ruta); });
            //                        break;
            //                    case "ListarSubFamiliaServicios":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Subfamilia_Servicios>(ruta); });
            //                        break;
            //                    case "ListarUbicacion":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Ubicacion>(ruta); });
            //                        break;
            //                    case "ListarUnidadesMedida":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Unidades_Medida>(ruta); });
            //                        break;
            //                    case "ListarValoresSat":
            //                        await Task.Run(async () => { await Conexion.GetDatosAsync<Valores_SAT>(ruta); });
            //                        break;
            //                }
            //            }

            //            BtnEliminaBBDD.IsEnabled = true;

            //            await DisplayAlert("Aviso", "Descargada y guardada la BBDD correctamente", "OK");
            //        }

            //        catch (Exception ex)
            //        {
            //            throw ex;
            //            //await DisplayAlert("Aviso", ex.Message, "OK");
            //        }

            //        DispositivosLista.IsVisible = true;
            //        popupLoadingView.IsVisible = false;
            //        activityIndicator.IsRunning = false;
            //}
            //}
        }

        //Elimina la BBDD por completo 
        public async void BtnEliminaBBDD_Clicked(object sender, EventArgs e)
        {
            if (Haypartesabiertos() == true)
            {
                await DisplayAlert("Aviso", "! Hay partes abiertos, cierre los partes antes de borrar la Base de Datos !", "OK");
            }
            else
            {
                var respuesta = await DisplayAlert("Aviso", "¿ Deseas borrar la base de datos ?", "Si", "Cancelar");
                if (respuesta == true)
                {
                    DispositivosLista.IsVisible = false;
                    popupLoadingView.IsVisible = true;
                    lblLoadingText.Text = "Borrando base de datos..";
                    activityIndicator.IsRunning = true;

                    RepositorySatApp BBDD = new RepositorySatApp();

                    try
                    {
                        BBDD.DeleteBBDD<Articulos>();
                        BBDD.DeleteBBDD<Cliente>();
                        BBDD.DeleteBBDD<Cliente_Maquina>();
                        BBDD.DeleteBBDD<Direccion_Cliente>();
                        BBDD.DeleteBBDD<Forma_Pago>();
                        BBDD.DeleteBBDD<Generales>();
                        BBDD.DeleteBBDD<Iva>();
                        BBDD.DeleteBBDD<Maquinas>();
                        BBDD.DeleteBBDD<Pais>();
                        BBDD.DeleteBBDD<Personal>();
                        BBDD.DeleteBBDD<Provincias>();
                        BBDD.DeleteBBDD<Regimen_IVA>();
                        BBDD.DeleteBBDD<SAT>();
                        BBDD.DeleteBBDD<SAT_Equipo>();
                        BBDD.DeleteBBDD<SAT_Lineas>();
                        BBDD.DeleteBBDD<Servicios>();
                        BBDD.DeleteBBDD<Valores_SAT>();

                        BBDD.CerrarConexion();
                        await DisplayAlert("Aviso", "! La base de datos se ha borrado correctamente !", "OK");
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine(a.Message);
                        await DisplayAlert("Aviso", "! Error al eliminar la base de datos !", "OK");
                    }
                    DispositivosLista.IsVisible = true;
                    popupLoadingView.IsVisible = false;
                    activityIndicator.IsRunning = false;
                }
            }
        }

        //Guarda la configuración del servidor y  la impresora en la BBDD interna
        private async void BtnConfiguracion_Clicked(object sender, EventArgs e)
        {
            //Busca la Impresora al Presionar el boton de Lupa
            if (Variables.ConfImpresora == true)
            {
                string IP = Servidor.Text;
                string PORT = Puerto.Text;
                string impresora = "No impresora";

                if (dispositivo != null)
                {
                    impresora = dispositivo.NativeDevice.ToString();
                }

                Conexiones datos = new Conexiones
                {
                    Codigo = 1,
                    IP = IP,
                    Puerto = PORT,
                    Dispositivo = impresora,
                    Bloquea_Parte = BloqueParte,
                };

                //Si no existe el registro lo guarda, si existe lo actualiza
                RepositorySatApp transferir = new RepositorySatApp();

                transferir.InsertOrUpdate(datos, 1);

                if (impresora.Equals("No impresora"))
                {
                    await DisplayAlert("Aviso", "Datos guardados:\n" + datos.IP + ",\n" + datos.Puerto + ",\n" + datos.Dispositivo, "OK");
                }
                else
                {
                    await Adaptador.DisconnectDeviceAsync(dispositivo);
                    await DisplayAlert("Aviso", "Datos guardados:\n" + datos.IP + ",\n" + datos.Puerto + ",\n" + datos.Dispositivo, "OK");
                }
                transferir.CerrarConexion();
            }

            //Comprueba que todos los campos este rellenos.
            if (string.IsNullOrEmpty(Servidor.Text) && (string.IsNullOrEmpty(Puerto.Text) && Variables.ConfImpresora == false))
            {
                await DisplayAlert("Aviso", "! Tiene que rellenar todos los datos !", "OK");
            }
            else
            {
                string IP = Servidor.Text;
                string PORT = Puerto.Text;
                string impresora = "No impresora";
                if (dispositivo != null)
                {
                    impresora = dispositivo.NativeDevice.ToString();
                }

                Conexiones datos = new Conexiones
                {
                    Codigo = 1,
                    IP = IP,
                    Puerto = PORT,
                    Dispositivo = impresora,
                    Bloquea_Parte = BloqueParte,
                };

                //Si no existe el registro lo guarda, si existe lo actualiza
                RepositorySatApp transferir = new RepositorySatApp();
                transferir.InsertOrUpdate(datos, 1);

                if (dispositivo != null)
                {
                    //Una vez guardado la mac del dispositivo lo desparejamos para que se empareje al ejecutar la aplicación
                    await Adaptador.DisconnectDeviceAsync(dispositivo);
                }

                transferir.CerrarConexion();
                await DisplayAlert("Aviso", "Datos guardados:\n" + datos.IP + ",\n" + datos.Puerto + ",\n" + datos.Dispositivo, "OK");
            }
            Variables.ConfImpresora = false;
        }


    }
}

//Cuenta Gmail

//appcenterecomputer@gmail.com
//Clave111

//Cuenta Github
//appcenterecomputer @gmail.com
//Clave: Clave!#2021

//Cuenta AppCenter
//appcenterecomputer@gmail.com
//Clave111