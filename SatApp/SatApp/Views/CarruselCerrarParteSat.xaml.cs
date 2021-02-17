using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarruselCerrarParteSat : ContentPage
    {
        readonly IAdapter adaptador;
        readonly IBluetoothLE bluetooth;
        readonly ObservableCollection<IDevice> Lista;
        IDevice dispositivo;
        string mac = null;


        readonly RepositorySatApp BBDD = new RepositorySatApp();

        SAT sat = new SAT();
        private List<SAT_Lineas> lineas = new List<SAT_Lineas>();
        private readonly SAT_Equipo equipo = new SAT_Equipo();

        public CarruselCerrarParteSat()
        {
            InitializeComponent();
            bluetooth = CrossBluetoothLE.Current;
            adaptador = CrossBluetoothLE.Current.Adapter;
            Lista = new ObservableCollection<IDevice>();

            var sati = BBDD.GetSat(Variables.DatosParte.N_Parte);

            sati.FechaEnvioApp = "1900-12-30 00:00:00.000";

            // Aqui se verifica si el parte esta abierto o cerrado
            if (Variables.RevisaParte)
            {

                if ((Variables.DatosParte.Revisar == true) && (Variables.DatosParte.Realizado == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                {
                    btnConfig.Source = "candadocerrado.png";
                    btnImprimir.IsEnabled = true;
                }

                if ((Variables.NoRealizado == true && Variables.DatosParte.Revisar == false) && (Variables.DatosParte.Realizado == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                {
                    btnConfig.Source = "candadocerrado.png";
                    btnImprimir.IsEnabled = true;
                    Variables.NoRealizado = false;
                }
            }

            BBDD.CerrarConexion();

        }

        /*
         * Añade todos los cambios realizados por el técnico en el parte y los guarda
         * Trabaja con las tablas SAT, SAT_Lineas, SAT_Equipo
         */
        public void FinalizarParte()
        {
            var sati = BBDD.GetSat(Variables.DatosParte.N_Parte);

            if (sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
            {

                Application.Current.MainPage.DisplayAlert("Aviso", "El parte ya se ha cerrado.", "OK");
                Imprimir(true);
            }
            else
            {
                RepositorySatApp database = new RepositorySatApp();

                var parte = Variables.DatosParte;
                var lineasSat = database.GetAllLineasParte(parte.N_Parte.ToString());  //var lineasSat = Variables.lineasParte;

                //var masvalores =database.GetAll<Valores_SAT>().FirstOrDefault();

                sat = database.Get<SAT>(parte.N_Parte);
                sat.Solucion = parte.Solucion;


                sat.Revisar = Variables.RevisaParte;

                if (Variables.RevisaParte == true)
                {

                    //sat.Realizado = false;
                    sat.Realizado = Variables.OptRealizado;

                }
                else
                    //sat.Realizado = true;
                    sat.Realizado = Variables.OptRealizado;


                //Añado estas dos lineas para que se guarden los valores de Realizado y Revisar en la variable global DatosParte.
                Variables.DatosParte.Realizado = sat.Realizado;
                Variables.DatosParte.Revisar = sat.Revisar;

                sat.Base1 = Variables.DatosParte.Base1;
                sat.Base2 = Variables.DatosParte.Base2;
                sat.Base3 = Variables.DatosParte.Base3;
                sat.Base4 = Variables.DatosParte.Base4;
                sat.Base5 = Variables.DatosParte.Base5;
                sat.Base_Total = Variables.DatosParte.Base_Total;

                sat.Iva1 = Variables.DatosParte.Iva1;
                sat.Iva2 = Variables.DatosParte.Iva2;
                sat.Iva3 = Variables.DatosParte.Iva3;
                sat.Iva4 = Variables.DatosParte.Iva4;
                sat.Iva5 = Variables.DatosParte.Iva5;
                sat.Iva_Total = Variables.DatosParte.Iva_Total;

                sat.TantoIva1 = Variables.DatosParte.TantoIva1;
                sat.TantoIva2 = Variables.DatosParte.TantoIva2;
                sat.TantoIva3 = Variables.DatosParte.TantoIva3;
                sat.TantoIva4 = Variables.DatosParte.TantoIva4;
                sat.TantoIva5 = Variables.DatosParte.TantoIva5;

                sat.Tanto_Equivalencia1 = Variables.DatosParte.Tanto_Equivalencia1;
                sat.Tanto_Equivalencia2 = Variables.DatosParte.Tanto_Equivalencia2;
                sat.Tanto_Equivalencia3 = Variables.DatosParte.Tanto_Equivalencia3;
                sat.Tanto_Equivalencia4 = Variables.DatosParte.Tanto_Equivalencia4;
                sat.Tanto_Equivalencia5 = Variables.DatosParte.Tanto_Equivalencia5;

                sat.Recargo_Equivalencia1 = Variables.DatosParte.Recargo_Equivalencia1;
                sat.Recargo_Equivalencia2 = Variables.DatosParte.Recargo_Equivalencia2;
                sat.Recargo_Equivalencia3 = Variables.DatosParte.Recargo_Equivalencia3;
                sat.Recargo_Equivalencia4 = Variables.DatosParte.Recargo_Equivalencia4;
                sat.Recargo_Equivalencia5 = Variables.DatosParte.Recargo_Equivalencia5;

                sat.Forma_Pago = Variables.DatosParte.FormaPago;

                sat.Recargo_Total = Variables.DatosParte.Recargo_Total;

                sat.Total = Variables.DatosParte.Total;

                sat.Observaciones = Variables.DatosParte.Observaciones;

                sat.EnviadoAPP = false;

                sat.PendienteRepuesto = Variables.OptPendienteRepuesto;
                sat.PendientePresupuesto = Variables.OptPendientePresupuesto;
                sat.PendienteRecambio = Variables.OptPendienteRecambio;
                sat.SinReparar = Variables.OptSinReparar;


                lineas = database.GetAllLineasParte(parte.N_Parte.ToString());


                var horafin = DateTime.Now;
                sat.HoraFinTarea = horafin;
                sat.Hora = horafin;
                sat.Fecha_Realizado = horafin.ToString("yyyy-MM-dd");
                sat.Fecha_Garantia = "1900-12-30 00:00:00.000";
                sat.FechaEnvioApp = "1900-12-30 00:00:00.000";

                database.Guardar(sat);

                btnImprimir.IsEnabled = true; //Habilita el boton de impresión

                btnConfig.Source = "candadocerrado.png";

                btnConfig.IsEnabled = false;

            }

            BBDD.CerrarConexion();
        }

        #region BusquedaDispositivo
        /*
         * Realiza la busqueda de la impresora
         */
        public async void BusquedaDispositivo(object objeto, EventArgs E)
        {
            try
            {
                RepositorySatApp impresora = new RepositorySatApp();

                List<Conexiones> parametros = impresora.GetAll<Conexiones>();

                foreach (var pa in parametros) //Obtiene los datos de la impresora configurada
                {
                    mac = pa.Dispositivo;
                }

                if (mac == "No impresora")
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "No hay impresoras configuradas", "OK");
                    ConfiguraImpresora();
                    return;
                }

                if (bluetooth.State == BluetoothState.On)
                {
                    popupLoadingView.IsVisible = true;
                    lblLoadingText.Text = "Enviando datos a la impresora...";
                    activityIndicator.IsRunning = true;
                    btnImprimir.IsEnabled = false;

                    Lista.Clear();
                    adaptador.ScanTimeout = 10000;
                    adaptador.ScanMode = ScanMode.Balanced;
                    int nvecesencontrado = 0; // Esta variable permite filtrar el numero de veces que encuentra el dispositivo. Aveces se bugea e imprime dos veces
                    adaptador.DeviceDiscovered += (obj, a) =>
                    {
                        Lista.Add(a.Device);

                        if (a.Device.NativeDevice.ToString().Equals(mac)) //Localiza la impresora que tiene configurada
                        {
                            dispositivo = a.Device as IDevice;
                            if (nvecesencontrado < 1) // Si el dispositivo ya lo ha encontrado no volvera a intentar conectarse una segunda vez
                            {
                                Autoconexion(dispositivo);
                                adaptador.StopScanningForDevicesAsync();
                                nvecesencontrado++;
                            }
                        }
                    };

                    if (!bluetooth.Adapter.IsScanning)
                    {
                        await adaptador.StartScanningForDevicesAsync();
                    }
                }

                if (dispositivo == null)
                {
                    Lista.Clear();
                    await Application.Current.MainPage.DisplayAlert("Aviso", "No se encuentra la impresora", "OK");
                    popupLoadingView.IsVisible = false;
                    activityIndicator.IsRunning = false;
                    btnImprimir.IsEnabled = true;
                }

                impresora.CerrarConexion();
            }
            catch (Exception s)
            {
                Console.WriteLine(s.Message);
            }
        }
        #endregion

        #region conexion
        /*
         * Como la impresora ya esta configurada se conecta solo a la impresora
         */
        public async void Autoconexion(IDevice impBT)
        {
            await adaptador.StopScanningForDevicesAsync();
            try
            {
                // Estas lineas conectan el dispositivo e imprimen el parte;
                var parameters = new ConnectParameters(forceBleTransport: true);
                await adaptador.ConnectToDeviceAsync(impBT, parameters);
                Lista.Clear();
                Lista.Add(dispositivo);
                bool imprimir = true; // IMPORTANTE Este booleano permite imprimir a la impresora
                Imprimir(imprimir);


            }
            catch (DeviceConnectionException e)
            {
                Console.WriteLine(e.Message);
                await Application.Current.MainPage.DisplayAlert("Aviso", "No es posible conectarse a la impresora", "OK");
                popupLoadingView.IsVisible = false;
                activityIndicator.IsRunning = false;
                btnImprimir.IsEnabled = true;
            }
        }

        #endregion
        /*
         * Mandamos a imprimir los datos del parte
         */
        public async void Imprimir(Boolean imprimir)
        {
            try
            {
                lblLoadingText.Text = "Imprimiendo ticket...";

                RepositorySatApp BBDD = new RepositorySatApp();
                var empresa = BBDD.Get<Generales>(1);
                var Nombre = empresa.Empresa;
                var nif = empresa.NIF;
                var direc = empresa.Direccion;
                var telf = empresa.Telefono1;
                var comercial = empresa.N_Comercial;

                //Obtenemos y guardamos en objetos la información que deseamos imprimir
                //Transformamos los datos en Bytes antes de enviarlos a la impresora

                var parte = Variables.DatosParte;
                var masDatos = BBDD.GetSat(parte.N_Parte);
                var masCliente = BBDD.Get<Cliente>(masDatos.Cliente);
                var masProvincia = BBDD.GetProvincias(masCliente.Provincia);
                var lineasParte = BBDD.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString());

                //Obtiene nombre comercial. De momento cliente no la quiere
                var codempresa = BBDD.Get<Generales>(0);

                //Hay que traerse la tabla comercialcliente
                // string masComercial = BBDD.GetComercialCliente(codempresa.Codigo, parte.CodigoCliente);

                string masPersonal = BBDD.GetNombreTecnico(masDatos.Realizado_Por);


                //Esta es la copia buena 28/01/2020
                BBDD.CerrarConexion();

                // Convertimos los strings a bytes y los imprimimos
                byte[] negrita = new byte[] { 0x1B, 0x21, 0x08 };
                byte[] normal = new byte[] { 0x1B, 0x21, 0x00 };
                byte[] negritamedia = new byte[] { 0x1B, 0x21, 0x20 };

                byte[] N_Parte = Encoding.ASCII.GetBytes("Numero Parte:       " + parte.N_Parte);
                byte[] Fecha = Encoding.ASCII.GetBytes("Fecha aviso:        " + parte.Fecha.ToShortDateString());
                byte[] HoraAviso = Encoding.ASCII.GetBytes("Hora aviso:         " + masDatos.Hora_Entrada.ToShortTimeString());
                byte[] FechaCerrado = Encoding.ASCII.GetBytes("Fecha realizado:    " + Convert.ToDateTime(masDatos.Fecha_Realizado.ToString()).ToShortDateString());
                var horainicio = masDatos.HoraInicioTarea.AddHours(1);
                var horafin = masDatos.HoraFinTarea.AddHours(1);


                byte[] Cliente = Encoding.ASCII.GetBytes("Cliente:           " + parte.NombreCliente);
                byte[] nifCli = Encoding.ASCII.GetBytes("DNI / NIF:          " + masCliente.NIF);
                byte[] Direccion = Encoding.ASCII.GetBytes("Direccion:          " + parte.DireccionParte);
                byte[] Poblacion = Encoding.ASCII.GetBytes("C.P / Poblacion:    " + masCliente.C_P);
                byte[] Provincia = Encoding.ASCII.GetBytes("Provincia:          " + masProvincia.Provincia);
                byte[] nomcomercial = Encoding.ASCII.GetBytes("Tecnico:            " + masPersonal);

                byte[] Anomalia = Encoding.ASCII.GetBytes("Anomalia:\n " + parte.AnomaliaParte);
                //byte[] Telefono = Encoding.ASCII.GetBytes("Telefono: " + parte.TelefonoCliente);
                byte[] Solucion = Encoding.ASCII.GetBytes("Trabajo efectuado:\n " + parte.Solucion);
                // byte[] Observaciones = Encoding.ASCII.GetBytes("Observaciones adicionales:\n " + parte.Observaciones);
                byte[] Pago = Encoding.ASCII.GetBytes("Forma de Pago: " + parte.FormaPago);
                byte[] Base = Encoding.ASCII.GetBytes("          Base imponible:         " + parte.Base_Total.ToString("N2"));
                byte[] valorIva = Encoding.ASCII.GetBytes("          IVA:                    " + parte.Iva_Total.ToString("N2"));
                byte[] Total = Encoding.ASCII.GetBytes("          Total:                  " + parte.Total.ToString("N2"));


                byte[] estrellitas = Encoding.ASCII.GetBytes("===============================================\n");
                byte[] separacontinua = Encoding.ASCII.GetBytes("_______________________________________________\n");
                byte[] separacion = Encoding.ASCII.GetBytes("------------------------------------------------\n");
                byte[] separaverticalFirma = Encoding.ASCII.GetBytes("Firma Trabajador:     |   Firma Cliente:\n");
                byte[] separavertical = Encoding.ASCII.GetBytes("                      | \n");
                byte[] salto = Encoding.ASCII.GetBytes("\n");
                byte[] firma1 = Encoding.ASCII.GetBytes("Firma Trabajador:");
                byte[] firma2 = Encoding.ASCII.GetBytes("Firma Cliente:");
                byte[] ArtServ = Encoding.ASCII.GetBytes("             Articulos y servicio:");
                byte[] Cabecera = Encoding.ASCII.GetBytes("Referencia   Precio   Cantidad   Importe    Iva\n");

                byte[] N_empresa = Encoding.ASCII.GetBytes("          " + Nombre + "\n");
                byte[] dic_empresa = Encoding.ASCII.GetBytes("          " + direc + "\n");
                byte[] telf_empresa = Encoding.ASCII.GetBytes("               Telf: " + telf + "\n");
                byte[] nif_empresa = Encoding.ASCII.GetBytes("               N.I.F: " + nif + "\n");
                byte[] Nombre_Comercial = Encoding.ASCII.GetBytes("       " + comercial);
                byte[] CabLineas = Encoding.ASCII.GetBytes("                PARTE TRABAJO\n");
                byte[] Cabdesg = Encoding.ASCII.GetBytes("        Desglose categorias impositivas\n");
                byte[] desglose = Encoding.ASCII.GetBytes("            Base     Iva    Importe\n");
                byte[] valordesglose = Encoding.ASCII.GetBytes("            " + parte.Base_Total + "      " + parte.TantoIva1 + "     " + parte.Iva_Total);
                byte[] CabTotales = Encoding.ASCII.GetBytes("               Totales finales:\n");
                byte[] gracias = Encoding.ASCII.GetBytes("    Le damos las gracias por su confianza\n");
                byte[] docvista = Encoding.ASCII.GetBytes("Este documento solo es una vista preliminar y no reemplaza al documento de facturación\n");


                var servicios = await dispositivo.GetServicesAsync();


                foreach (var l in servicios)
                {
                    if (l.Name == "Unknown Service") //Obtiene el servicio del que depende la caracteristica que nos permite enviar datos a la impresora
                    {
                        var server = l as IService;
                        var caracteristica = await server.GetCharacteristicsAsync();


                        foreach (var m in caracteristica)
                        {
                            if (m.Name == "Unknown characteristic") //Localiza la caracteristica que se usa para enviar datos a la impresora
                            {
                                var carac = m as ICharacteristic;

                                //Cabecera ticket
                                if (carac.CanWrite && imprimir)  // Este if comprueba si el proximo caracter es imprimible y si el booleano es true o false
                                {                                // SIN ESTE IF NO IMPRIME 
                                    await carac.WriteAsync(negrita);
                                    await carac.WriteAsync(N_empresa);
                                    await carac.WriteAsync(dic_empresa);
                                    await carac.WriteAsync(telf_empresa);
                                    await carac.WriteAsync(nif_empresa);//fin cabecera ticket

                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    //Datos parte
                                    await carac.WriteAsync(normal);
                                    await carac.WriteAsync(N_Parte);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Fecha);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(HoraAviso);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(FechaCerrado);

                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    //Datos cliente
                                    await carac.WriteAsync(Cliente);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(nifCli);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Direccion);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Poblacion);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Provincia);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(nomcomercial);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    //Incidencia
                                    await carac.WriteAsync(Anomalia);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Solucion);
                                    await carac.WriteAsync(salto);
                                    // await carac.WriteAsync(Observaciones);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    //Cabecera lineas tarea
                                    await carac.WriteAsync(estrellitas);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(CabLineas);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(ArtServ); //Articulos y servicios
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Cabecera); //Cabecera lineasSat
                                    await carac.WriteAsync(separacontinua);
                                    await carac.WriteAsync(salto);

                                    foreach (var h in lineasParte) //Lineas partesat
                                    {
                                        var precio = Math.Round(h.Precio, 2);
                                        var importe = Math.Round(h.Importe, 2);
                                        byte[] desc = Encoding.ASCII.GetBytes(h.Descripcion + "\n");
                                        byte[] refer = Encoding.ASCII.GetBytes(" " + h.Referencia.ToString());
                                        byte[] espacio = Encoding.ASCII.GetBytes(" ");

                                        byte[] prec = Encoding.ASCII.GetBytes(precio.ToString("N2") + "       ");
                                        byte[] cant = Encoding.ASCII.GetBytes(h.Cantidad.ToString());
                                        byte[] imp = Encoding.ASCII.GetBytes(importe.ToString("N2") + "        ");
                                        byte[] iva = Encoding.ASCII.GetBytes(h.Iva.ToString() + "\n");
                                        await carac.WriteAsync(desc);

                                        if (CuentaDigito(h.Referencia) <= 2)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("     ");
                                            await carac.WriteAsync(refer);
                                            await carac.WriteAsync(espacio);
                                        }
                                        else
                                        {
                                            await carac.WriteAsync(refer);
                                        }

                                        if (CuentaDigito(precio) >= 5)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("  ");
                                        }
                                        else if (CuentaDigito(precio) == 4)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("   ");
                                        }
                                        else if (CuentaDigito(precio) == 3)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("    ");
                                        }
                                        else if (CuentaDigito(precio) == 2)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("     ");
                                        }
                                        else if (CuentaDigito(precio) == 1 || CuentaDigito(precio) == 0)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("      ");
                                        }
                                        await carac.WriteAsync(espacio);
                                        await carac.WriteAsync(prec);
                                        await carac.WriteAsync(cant);
                                        if (CuentaDigito(importe) >= 5)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("  ");
                                        }
                                        else if (CuentaDigito(importe) == 4)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("    ");
                                        }
                                        else if (CuentaDigito(importe) == 3)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("     ");
                                        }
                                        else if (CuentaDigito(importe) == 2)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("      ");
                                        }
                                        else if (CuentaDigito(importe) == 1 || CuentaDigito(importe) == 0)
                                        {
                                            espacio = Encoding.ASCII.GetBytes("       ");
                                        }
                                        await carac.WriteAsync(espacio);
                                        await carac.WriteAsync(imp);
                                        await carac.WriteAsync(iva);
                                    }

                                    //Desglose categorias impositivas
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Cabdesg);
                                    await carac.WriteAsync(desglose);
                                    await carac.WriteAsync(separacontinua);
                                    await carac.WriteAsync(valordesglose);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    //Totales finales
                                    await carac.WriteAsync(CabTotales);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Base);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(valorIva);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(Total);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    //Firmas
                                    await carac.WriteAsync(separacontinua);
                                    await carac.WriteAsync(separaverticalFirma);
                                    await carac.WriteAsync(separavertical);
                                    await carac.WriteAsync(separavertical);
                                    await carac.WriteAsync(separavertical);
                                    await carac.WriteAsync(separavertical);
                                    await carac.WriteAsync(separavertical);
                                    await carac.WriteAsync(separavertical);
                                    await carac.WriteAsync(separacontinua);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(negritamedia);
                                    await carac.WriteAsync(Nombre_Comercial);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(normal);
                                    await carac.WriteAsync(gracias);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(docvista);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);
                                    await carac.WriteAsync(salto);

                                    var respuesta = await Application.Current.MainPage.DisplayAlert("Aviso", "Si desea una copia, pulse el botón de imprimir", "Ok", "Cancelar");

                                    if (respuesta == true)  // Si la respuesta es Cancelar, salimos del bucle
                                    {
                                        Imprimir(imprimir);

                                    }
                                    else
                                    {
                                        imprimir = false;
                                        popupLoadingView.IsVisible = false;
                                        activityIndicator.IsRunning = false;
                                        btnImprimir.IsEnabled = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }


                await adaptador.DisconnectDeviceAsync(dispositivo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /*
         * Si la impresora no esta configurada te envia a la pantalla de configuración
         */
        public async void ConfiguraImpresora()
        {
            Variables.ConfImpresora = true;
            await Navigation.PushModalAsync(new Configuracion());
        }

        public int CuentaDigito(decimal dato)
        {
            int n = Convert.ToInt32(Math.Round(dato, 0));
            int cont = 0;
            int res;

            while (n > 0)
            {
                res = n % 10;
                cont++;
                n /= 10;
            }

            return cont;

        }
        public void VistaOptionCierre(object sender, EventArgs args)
        {
            try
            {
                if (Variables.DatosParte.Revisar == true)
                {
                    Application.Current.MainPage.DisplayAlert("Aviso", "El parte ya se ha cerrado.", "OK");
                }
                else
                {
                    Navigation.PushAsync(new OpcionButton());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}