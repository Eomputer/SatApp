using Rg.Plugins.Popup.Services;
using SatApp.Modelos;
using SatApp.Repository;
using SatApp.ServiciosDB;
using SatApp.VistasModelos;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Funciones = SatApp.Repository.Funciones;

namespace SatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarruselArtServParteSat : ContentPage
    {
        readonly Funciones funciones = new Funciones();
        SAT_Lineas nuevaLinea;

        public delegate void AddItemDelegate();
        public bool bolEsArticulo = false;

        public CarruselArtServParteSat()
        {
            InitializeComponent();

            bolEsArticulo = true;

            if (Variables.BloqueaParte)
            {
                RepositorySatApp BBDD = new RepositorySatApp();
                var sati = BBDD.GetSat(Variables.DatosParte.N_Parte);
                if (Variables.RevisaParte)
                {

                    if ((Variables.DatosParte.Revisar == true) && (Variables.DatosParte.Realizado == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                    {
                        Busqueda.IsEnabled = false;
                        switchVarios.IsEnabled = false;
                        txtReferencia.IsEnabled = false;
                        txtNombre.IsEnabled = false;
                        txtBase.IsEnabled = false;
                        txtCantidad.IsEnabled = false;
                        txtDto.IsEnabled = false;
                        txtTantoPorcierto.IsEnabled = false;
                        btnListarArticulos.IsEnabled = false;
                        btnListarServicios.IsEnabled = false;
                        imgAnadir.IsEnabled = false;
                        imgQR.IsEnabled = false;
                        ListadoLineas.IsEnabled = false;
                        imgBuscarArt.IsEnabled = false;
                    }

                }
                else if (!Variables.RevisaParte)
                {
                    if ((Variables.DatosParte.Realizado == true) && (Variables.DatosParte.Revisar == false) && sati.FechaEnvioApp == "1900-12-30 00:00:00.000")
                    {
                        Busqueda.IsEnabled = false;
                        switchVarios.IsEnabled = false;
                        txtReferencia.IsEnabled = false;
                        txtNombre.IsEnabled = false;
                        txtBase.IsEnabled = false;
                        txtCantidad.IsEnabled = false;
                        txtDto.IsEnabled = false;
                        txtTantoPorcierto.IsEnabled = false;
                        btnListarArticulos.IsEnabled = false;
                        btnListarServicios.IsEnabled = false;
                        imgAnadir.IsEnabled = false;
                        imgQR.IsEnabled = false;
                        ListadoLineas.IsEnabled = false;
                        imgBuscarArt.IsEnabled = false;
                    }
                }
                BBDD.CerrarConexion();
            }
        }

        private void Articulos_Clicked(object sender, EventArgs e)
        {
            //Al hacer click al boton queda por defecto para las busquedas de Articulos
            try
            {
                bolEsArticulo = true;
                btnListarArticulos.BackgroundColor = Color.FromHex("#0a4343");
                btnListarServicios.BackgroundColor = Color.FromHex("#137F7F");

                txtReferencia.Text = "";
                txtNombre.Text = "";
                txtBase.Text = "";
                txtCantidad.Text = "";
                txtDto.Text = "";
                txtTantoPorcierto.Text = "";
                Busqueda.Text = "";

                switchVarios.IsToggled = false;
            }
            catch (Exception ex)
            {               
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }
        }

        //Al hacer click al boton queda por defecto para las busquedas de Servicios
        private void Servicios_Clicked(object sender, EventArgs e)
        {
            try
            {
                bolEsArticulo = false;
                btnListarServicios.BackgroundColor = Color.FromHex("#0a4343");
                btnListarArticulos.BackgroundColor = Color.FromHex("#137F7F");

                txtReferencia.Text = "";
                txtNombre.Text = "";
                txtBase.Text = "";
                txtCantidad.Text = "";
                txtDto.Text = "";
                txtTantoPorcierto.Text = "";
                Busqueda.Text = "";

                switchVarios.IsToggled = false;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }

        }

        //Código para el código de barra
        void QR_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanCod = new ZXingScannerPage();
                string codigoBarras = null;

                scanCod.OnScanResult += async (result) =>
                {
                    scanCod.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopModalAsync();
                        codigoBarras = result.Text;
                        RepositorySatApp database = new RepositorySatApp();

                        var articulo = database.GetArticuloPorCodBarras(codigoBarras);


                        if (articulo != null)
                        {
                            txtReferencia.Text = articulo.Referencia.ToString();
                            txtNombre.Text = articulo.Articulo;
                            txtBase.Text = articulo.PVP.ToString();
                            txtCantidad.Text = "1";
                            txtTantoPorcierto.Text = articulo.IVA.ToString();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Escaner", "El articulo no existe", "OK");
                        }
                        database.CerrarConexion();
                    });

                    await Navigation.PushModalAsync(scanCod);
                };
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                int Referencia;

                RepositorySatApp database = new RepositorySatApp();

                Articulos articulo = new Articulos();

                if (!string.IsNullOrEmpty(txtReferencia.Text))
                {
                    //Busqueda de articulos en caso de estar activo el botón Articulos
                    if (bolEsArticulo)
                    {
                        if (int.TryParse(txtReferencia.Text, out Referencia))
                        {
                            articulo = database.GetArticuloReferencia(txtReferencia.Text);//Busqueda por Referencia Or ReferenciaProveddor                    
                        }


                        if (articulo == null)
                        {

                            await Application.Current.MainPage.DisplayAlert("Aviso", "No se encontro ningun articulo con esa referencia o número de proveedor.", "OK");

                            txtReferencia.Text = "";
                        }

                        else
                        {
                            txtNombre.Text = articulo.Articulo;
                            txtBase.Text = articulo.PVP.ToString();
                            txtCantidad.Text = "1";
                            txtTantoPorcierto.Text = articulo.IVA.ToString();
                        }
                    }

                    else//Busqueda de articulos en caso de estar activo el botón Servicios
                    {
                        if (int.TryParse(txtReferencia.Text, out Referencia))
                        {
                            var servicio = database.ObtenerServicio(int.Parse(txtReferencia.Text));

                            if (servicio != null)
                            {
                                txtNombre.Text = servicio.Descripcion;
                                txtBase.Text = servicio.Precio.ToString();
                                txtCantidad.Text = "1";
                                txtTantoPorcierto.Text = servicio.IVA.ToString();
                            }
                            else
                                await Application.Current.MainPage.DisplayAlert("Aviso", "No existe el servicio.", "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Aviso", "Referencia incorrecta", "OK");
                            txtReferencia.Text = "";
                        }
                    }
                }
                database.CerrarConexion();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso ", ex.Message, " OK");
            }

        }

        private void Anadir_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Añade un Articulo o Servicio
                if (!string.IsNullOrEmpty(txtReferencia.Text))
                {
                    if (!string.IsNullOrEmpty(txtNombre.Text) || !string.IsNullOrEmpty(txtCantidad.Text) || !string.IsNullOrEmpty(txtBase.Text))
                    {
                        nuevaLinea = new SAT_Lineas();
                        RepositorySatApp database = new RepositorySatApp();

                        Articulos Articulo;

                        if (bolEsArticulo)//Busqueda Articulos
                        {
                            Articulo = database.GetArticuloReferencia(txtReferencia.Text);//Busqueda por Referencia
                            nuevaLinea.Referencia = Articulo.Referencia;
                            nuevaLinea.Descripcion = txtNombre.Text;
                            nuevaLinea.Cantidad = decimal.Parse(txtCantidad.Text);
                            nuevaLinea.Precio = decimal.Parse(txtBase.Text);
                            nuevaLinea.PrecioCoste = Articulo.Precio_Coste;

                            if (!string.IsNullOrEmpty(txtDto.Text))

                                nuevaLinea.DTO = decimal.Parse(txtDto.Text);


                            nuevaLinea.Iva = decimal.Parse(txtTantoPorcierto.Text);
                            nuevaLinea.Recargo = decimal.Parse(Articulo.Recargo.ToString());
                            nuevaLinea.Importe = (nuevaLinea.Precio - (nuevaLinea.Precio * nuevaLinea.DTO / 100)) * nuevaLinea.Cantidad;

                            nuevaLinea.N_Parte = Variables.DatosParte.N_Parte;

                            nuevaLinea.Almacen = 1;

                            nuevaLinea.Orden_Linea = database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString()).Count + 1;
                            nuevaLinea.TipoReferencia = "A";
                            nuevaLinea.VieneDeAPP = true;
                        }
                        else//Busqueda por referencia
                        {
                            nuevaLinea.Referencia = decimal.Parse(txtReferencia.Text);
                            nuevaLinea.Descripcion = txtNombre.Text;
                            nuevaLinea.Cantidad = decimal.Parse(txtCantidad.Text);
                            nuevaLinea.Precio = decimal.Parse(txtBase.Text);

                            if (!string.IsNullOrEmpty(txtDto.Text))

                                nuevaLinea.DTO = decimal.Parse(txtDto.Text);
                                nuevaLinea.Iva = decimal.Parse(txtTantoPorcierto.Text);
                                nuevaLinea.Importe = (nuevaLinea.Precio - (nuevaLinea.Precio * nuevaLinea.DTO / 100)) * nuevaLinea.Cantidad;
                                nuevaLinea.N_Parte = Variables.DatosParte.N_Parte;
                                nuevaLinea.Orden_Linea = database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString()).Count + 1;
                                nuevaLinea.TipoReferencia = "S";
                                nuevaLinea.VieneDeAPP = true;
                        }

                        var btnAnadir = sender as Button;

                        var vm = BindingContext as ArtServParteSAT_VM;


                        vm?.EliminarLinea.Execute(nuevaLinea); // Elimina la linea de Observable Collection (la lista de articulos de la vista)
                        database.EliminarLineaParte(nuevaLinea); // Elimina la linea de la BD

                        var lineas = new ObservableCollection<SAT_Lineas>(database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString())); // Crea una nueva coleccion

                        database.EliminarLineasParte(Variables.DatosParte.N_Parte.ToString()); // Elimina todas las lineas del parte

                        // Actualizala bd
                        var ordenLinea = 1;
                        foreach (var item in lineas)
                        {
                            item.Orden_Linea = ordenLinea;
                            database.Insert(item);
                            ordenLinea++;
                        }

                        vm.LineasSAT = new ObservableCollection<SAT_Lineas>(database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString()));
                        funciones.CalcularParte(Variables.DatosParte.N_Parte);

                        vm?.AnadirLinea.Execute(nuevaLinea);
                        database.Insert(nuevaLinea);


                        funciones.CalcularParte(Variables.DatosParte.N_Parte);

                        txtReferencia.Text = "";
                        txtNombre.Text = "";
                        txtBase.Text = "";
                        txtCantidad.Text = "";
                        txtDto.Text = "";
                        txtTantoPorcierto.Text = "";

                        Busqueda.Text = "";

                        if (switchVarios.IsToggled == true)

                            switchVarios.IsToggled = false;
                        nuevaLinea = null;
                        database.CerrarConexion();
                    }
                    else
                        Application.Current.MainPage.DisplayAlert("Aviso", "Por favor, revise los datos introducidos. Hay valores incorrectos", "OK");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Aviso", "No hay articulo o servicio para añadir", "OK");
                    txtReferencia.Text = "";
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso ", ex.Message, " OK");
            }

        }

        public void OnPopupUnitsCallback(object sender, EventArgs e)
        {
            try
            {
                txtReferencia.Text = Variables.Referencia;

                if (!string.IsNullOrEmpty(txtReferencia.Text))
                {
                    RepositorySatApp database = new RepositorySatApp();

                    int referencia = int.Parse(txtReferencia.Text);
                    if (bolEsArticulo)
                    {
                        var articulo = database.Get<Articulos>(referencia);
                        txtNombre.Text = articulo.Articulo;
                        txtBase.Text = articulo.PVP.ToString();
                        txtCantidad.Text = "1";
                        txtTantoPorcierto.Text = articulo.IVA.ToString();
                    }
                    else
                    {
                        var servicio = database.ObtenerServicio(decimal.Parse(txtReferencia.Text));
                        txtNombre.Text = servicio.Descripcion;
                        txtBase.Text = servicio.Precio.ToString();
                        txtCantidad.Text = "1";
                        txtTantoPorcierto.Text = servicio.IVA.ToString();
                    }

                    database.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso ", ex.Message, " OK");
            }
        }

        public void OnPopupUnitsCallbackVarios(object sender, EventArgs e)
        {
            try
            {
                var x = sender as PopupVarios;
                var iva = x.iva;
                if (iva != null)
                {
                    txtTantoPorcierto.Text = iva.Porcentaje.ToString();
                    RepositorySatApp database = new RepositorySatApp();
                    var nuevoIva = database.ObtenerRecargoIva(iva.Codigo);

                    nuevaLinea.Recargo = nuevoIva.Recargo;
                    nuevaLinea.Iva = iva.Porcentaje;

                    database.CerrarConexion();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Aviso", "Forma de Pago incorrecta.", "OK");
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso ", ex.Message, " OK");
            }

        }

        private async void Busqueda_SearchButtonPressed(object sender, EventArgs e)
        {
            //Busqueda de Articulo o Servicio por nombre
            RepositorySatApp database = new RepositorySatApp();

            if (!string.IsNullOrEmpty(Busqueda.Text))
            {
                try
                {
                    if (bolEsArticulo)//Busqueda por Articulo
                    {
                        var listado = database.GetArticulosPorConsulta(Busqueda.Text);
                        PopupArticulosServicios popupA = new PopupArticulosServicios(listado, bolEsArticulo);
                        popupA.CallbackEvent += OnPopupUnitsCallback;
                        await PopupNavigation.Instance.PushAsync(popupA);
                    }
                    else //Busqueda por Servicio
                    {
                        var listado = database.GetServiciosPorConsulta(Busqueda.Text);
                        PopupArticulosServicios popupS = new PopupArticulosServicios(listado, false);
                        popupS.CallbackEvent += OnPopupUnitsCallback;
                        await PopupNavigation.Instance.PushAsync(popupS);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Aviso", ex.Message, "OK");
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Aviso", "No hay articulo o servicio para buscar.", "OK");
            database.CerrarConexion();
        }

        async void BuscarIVA(object sender, ItemTappedEventArgs e)
        {
            //Busqueda el Iva articulo o servicio
            if (!string.IsNullOrEmpty(txtTantoPorcierto.Text))
            {
                try
                {
                    var popup = new PopupVarios("IVA");
                    popup.CallbackEvent += OnPopupUnitsCallbackVarios;
                    await PopupNavigation.Instance.PushAsync(popup);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Aviso", ex.Message, "OK");
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Aviso", "No hay articulo o servicio para buscar.", "OK");
        }

        private void EliminarLinea_Tapped(object sender, EventArgs e)
        {
            //Elimina la linea de un parte
            try
            {
                var imagen = sender as Image;
                var linea = imagen?.BindingContext as SAT_Lineas;
                var vm = BindingContext as ArtServParteSAT_VM;


                vm?.EliminarLinea.Execute(linea);

                RepositorySatApp database = new RepositorySatApp();
                database.EliminarLineaParte(linea);

                var lineas = new ObservableCollection<SAT_Lineas>(database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString()));

                database.EliminarLineasParte(Variables.DatosParte.N_Parte.ToString());

                var ordenLinea = 1;
                foreach (var item in lineas)
                {
                    item.Orden_Linea = ordenLinea;
                    database.Insert(item);
                    ordenLinea++;
                }

                vm.LineasSAT = new ObservableCollection<SAT_Lineas>(database.GetAllLineasParte(Variables.DatosParte.N_Parte.ToString()));

                funciones.CalcularParte(Variables.DatosParte.N_Parte);

                database.CerrarConexion();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }

        }


        private void CargaArticuloVarios(object sender, ToggledEventArgs e)
        {
            //Añade un articulo varios por defecto cuando activa el switch
            try
            {
                RepositorySatApp database = new RepositorySatApp();

                Articulos item = database.ObtenerArticuloVarios();
                if (item != null)
                {
                    if (switchVarios.IsToggled)
                    {
                        txtReferencia.Text = item.Referencia.ToString();
                        txtNombre.Text = item.Articulo;
                        txtBase.Text = item.PVP.ToString();
                        txtCantidad.Text = "1";
                        txtTantoPorcierto.Text = item.IVA.ToString();

                        bolEsArticulo = true;
                        btnListarArticulos.BackgroundColor = Color.FromHex("#0a4343");
                        btnListarServicios.BackgroundColor = Color.FromHex("#137F7F");

                        switchVarios.OnColor = Color.FromHex("#137F7F");
                    }
                    else
                    {
                        txtReferencia.Text = "";
                        txtNombre.Text = "";
                        txtBase.Text = "";
                        txtCantidad.Text = "";
                        txtDto.Text = "";
                        txtTantoPorcierto.Text = "";
                        Busqueda.Text = "";
                    }
                }
                database.CerrarConexion();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }

        }

        private void ObtieneFoco(object sender, EventArgs e)
        {
            //Obtiene el foco de buscar por nombre.
            try
            {
                if (switchVarios.IsToggled == true)
                    switchVarios.IsToggled = false;
                    txtReferencia.Text = "";
                    txtNombre.Text = "";
                    txtBase.Text = "";
                    txtCantidad.Text = "";
                    txtDto.Text = "";
                    txtTantoPorcierto.Text = "";
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }

        }

        private void ListadoLineas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var lineasparte = (SAT_Lineas)e.Item;
                if (lineasparte.TipoReferencia.Equals("A"))
                {
                    bolEsArticulo = true;
                }
                else
                {
                    bolEsArticulo = false;
                }
                txtReferencia.Text = lineasparte.Referencia.ToString();
                txtCantidad.Text = lineasparte.Cantidad.ToString();
                txtDto.Text = lineasparte.DTO.ToString();
                txtTantoPorcierto.Text = lineasparte.Iva.ToString();
                txtNombre.Text = lineasparte.Descripcion;
                txtBase.Text = lineasparte.Precio.ToString();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Aviso", ex.Message, "OK");
            }

        }




    }
}