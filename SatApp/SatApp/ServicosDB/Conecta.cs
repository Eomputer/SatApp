using Newtonsoft.Json;
using SatApp.Modelos;
using SatApp.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SatApp.ServiciosDB
{
    public class Conecta
    {
        public HttpClient Client = null;
        string Dns = "";

        //Constructor de petición al webservice
        //Se le pasa la url y el formato en que recibira los datos
        public Conecta()
        {

            //Estructura de protocolo para conexión segura 
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            //Obtenemos la url donde se aloja el webservice
            RepositorySatApp DatosServidor = new RepositorySatApp();

            var Datos_Comunicacion = DatosServidor.GetAll<Conexiones>();

            if (Datos_Comunicacion != null)//Cuando se instala la app por primera vez la tabla Conexiones no existe y devuelve
            {
                if (Datos_Comunicacion.Count != 0)//Una vez que esta instalada la app ya existe la tabla conexiones, pero solo entrará cuando existan datos.
                {
                    foreach (var l in Datos_Comunicacion)
                    {
                        Variables.ServerUrl = ("https://" + l.IP + ":" + l.Puerto + "/");
                    }

                    //Parametros de conexión. Si el server requiere conexion segura obtiene un certificado
                    Client = new HttpClient
                    {
                        BaseAddress = new Uri(Variables.ServerUrl)
                    };
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.Timeout = TimeSpan.FromSeconds(300);
                }
            }
        }

        // Solicita los datos comunes a todos los técnicos
        // Recupera un JSON con los datos

        public async Task GetDatosAsync<T>(string direccion) where T : class
        {
            try
            {
                Variables.ErrorTransDatos = 0;

                HttpResponseMessage response = await Client.GetAsync(direccion);
                Console.WriteLine("SUCCESS: " + response.IsSuccessStatusCode);
                Console.WriteLine("REQUEST: " + response.RequestMessage);
                Console.WriteLine("STATUS: " + response.StatusCode);
                Console.WriteLine("HEADERS: " + response.Headers);
                Console.WriteLine("CONTENT: " + response.Content);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var Datos = await response.Content.ReadAsStringAsync();

                    List<T> Tabla = JsonConvert.DeserializeObject<List<T>>(Datos);

                    RepositorySatApp trasfiere = new RepositorySatApp();

                    trasfiere.SaveAsync<T>(Tabla);
                }
                else
                {
                    Console.WriteLine("Error en la conexión");
                }
            }
            catch (Exception e)
            {
                Variables.ErrorTransDatos = 1;
                await Application.Current.MainPage.DisplayAlert("Aviso", "Error en la comunicación:" + e.Message, "OK");
            }
        }

        /*
         * Solicita los datos de los partes del técnico que esta usando la aplicación
         * Manda como parametros el código y contraseña del usuario
         * Recupera un JSON con los datos
         */
        public async Task PostDatosAsync<T>(int usuario, string contraseña, string direccion) where T : class
        {
            RepositorySatApp trasfiere = new RepositorySatApp();

            try
            {
                Variables.ErrorTransDatos = 0;

                var contra = new Clases.Tecnico
                {
                    Codigo = Convert.ToInt32(usuario),
                    Contraseña = contraseña
                };

                string contenido = JsonConvert.SerializeObject(contra);

                var content = new StringContent(contenido, Encoding.UTF8, "application/json");

                var url = Variables.ServerUrl + direccion;

                HttpResponseMessage response = await Client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var Datos = await response.Content.ReadAsStringAsync();

                    List<T> Tabla = JsonConvert.DeserializeObject<List<T>>(Datos);

                    trasfiere.Save<T>(Tabla);

                    response.Dispose();

                    if (direccion == "api/PrecioClienteFiltrado")
                    {
                        Variables.Cargadatos = true;
                    }
                }
            }
            catch(Exception ex)
            {
                //En caso que hubiese un error en la descarga revertimos los cambios.
                BorrarBD();
                Variables.ErrorTransDatos = 1;
                await Application.Current.MainPage.DisplayAlert("Aviso", "Error en la comunicación:" + ex.Message, "OK");
            }
        }

        public async Task PostDatosUpdateAsync<T>(string direccion) where T : class
        {
            RepositorySatApp trasfiere = new RepositorySatApp();

            try
            {
                //Una vez que tengo los partes en el Teléfono procedo a actualiarlo en Pirineos la variables EnvioApp
                var recibepartes = trasfiere.PartesSinEnviar<T>();

                foreach (var partes in recibepartes)
                {
                    var parte = new SAT
                    {
                        N_Parte = partes.N_Parte
                    };

                    string contenido = JsonConvert.SerializeObject(partes);

                    var content = new StringContent(contenido, Encoding.UTF8, "application/json");

                    var url = Variables.ServerUrl + direccion;

                    HttpResponseMessage response = await Client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Update");
                    }
                }                
            }
            catch
            {
                await DisplayAlert("Transmisión", "Error [EnviadoAPP] no se ha actualizado", "OK");
            }
        }
        

        //Busca todos los partes asociados al personal
        public bool ExisteParte(SAT obj)
        {
            try
            {
                RepositorySatApp datos = new RepositorySatApp();
                var list = datos.GetAllSAT(Convert.ToInt32(Variables.CodigoPersonal));
                Console.WriteLine(list.ToString());
                foreach (var item in list)
                {
                    if (item.N_Parte == obj.N_Parte)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Obtiene una lista con las lineas sat de los partes cerrados
        public bool ExisteParteLineas(SAT_Lineas obj)
        {
            try
            {
                RepositorySatApp datos = new RepositorySatApp();
                var list = datos.LineasSatCerrado<SAT_Lineas>(obj.N_Parte);
                Console.WriteLine(list.ToString());
                foreach (var item in list)
                {
                    if (item.N_Parte == obj.N_Parte && item.Descripcion.Equals(obj.Descripcion))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Solicita al servidor actualizar datos
        //Manda un JSON con los datos que se han de actualizar

        public async Task<bool> UpdateDatosAsync(string direccion, Object Dato)
        {
            Variables.ErrorTransDatos = 0;

            //Serializamos la información nueva en formato JSON
            var datos = JsonConvert.SerializeObject(Dato);
            var informacion = new StringContent(datos, Encoding.UTF8, "application/json");
            var url = Variables.ServerUrl + direccion;
            try
            {
                HttpResponseMessage response = await Client.PostAsync(url, informacion);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    bool resultado = false;

                    var respuesta = await response.Content.ReadAsStringAsync();

                    response.Dispose();

                    if (respuesta.Contains("PERFECTO"))
                    {
                        resultado = true;
                    }
                    else if (respuesta.Contains("OK"))
                    {
                        resultado = true;

                    }
                    else
                    {
                        resultado = false;
                    }

                    return resultado;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception a)
            {
                Variables.ErrorTransDatos = 1;
                Console.WriteLine(a.Message);

                return false;
            }
        }


        //Verifica las credenciales Usuario y Contraseña
        public async Task<bool> Acredita(int usuario, string contraseña)
        {

            Variables.ErrorLogin = 0;

            var contra = new Clases.Tecnico
            {
                Codigo = usuario,
                Contraseña = contraseña
            };

            string contenido = JsonConvert.SerializeObject(contra);


            var content = new StringContent(contenido, Encoding.UTF8, "application/json");

            try
            {
                var url = Variables.ServerUrl + "api/Descarga/Login";

                HttpResponseMessage response = await this.Client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Variables.BoolAcceso = true;//Acceso correcto
                    response.Dispose();//Se destruye el objeto. Para volver a utilizar nuevamente                   
                }
                else
                {
                    Variables.BoolAcceso = false;//Acceso denegado
                    response.Dispose();//Se destruye el objeto. Para volver a utilizar nuevamente
                }
            }
            catch
            {
                return false;
            }

            return Variables.BoolAcceso;
        }


        public async Task<int> GetNParte()
        {
            try
            {
                Variables.ErrorTransDatos = 0;
                var url = Variables.ServerUrl + "api/MaximoParteApp";
                HttpResponseMessage response = await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var Datos = await response.Content.ReadAsStringAsync();
                    return Convert.ToInt32(Datos);
                }
                else
                {
                    Console.WriteLine("Error en la conexión");
                    return 0;
                }
            }
            catch (Exception e)
            {
                Variables.ErrorTransDatos = 1;
                Console.WriteLine(e.Message);
                await Application.Current.MainPage.DisplayAlert("Aviso", "Error en el numero de fichaje" + e.Message, "OK");
                return 0;
            }
        }

        public bool AccesoInternet()
        {
            try
            {
                //*********Comprobamos si tenemos conexión a Internet**********//
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    Variables.ExisteInternet = false;//No hay conexión a Internet
                }
                else
                {
                    Variables.ExisteInternet = true;;//Si hay conexión a Internet
                }
            }
            catch
            {
                
            }
            return Variables.ExisteInternet;

        }

        //Esta función verifica que el HostName este bien escrito que apunte al Dns correcto. Simplemente se consulta a una tabla pequeña para comprobar la conexión.
        public async Task<bool> ValidoHostName<T>() where T : class
        {
            try
            {
                //Jugamos con la pestaña Servidor y Login
                if (!string.IsNullOrEmpty(Variables.Servidor))//Si no esta vacia la variable esta en la pestaña de configuracion, esto para controlar que salgas y entres de app, la variable ya esta vacía
                {
                    //Variables.Dns = Variables.ServerUrl + "api/Descarga/ListarIva";
                    Dns = "https://" + Variables.Servidor + ":" + Variables.Puerto + "/api/Descarga/ListarIva";
                }
                else //En caso contrario si entras directamente al Login Variables.Servidor esta vacía, entonces los datos los recoje directamente de la tabla conexiones.
                {
                    Variables.Dns = Variables.ServerUrl + "api/Descarga/ListarIva";
                }

                HttpResponseMessage response = await Client.GetAsync(Dns);

                if (response.IsSuccessStatusCode)
                {
                    var Datos = await response.Content.ReadAsStringAsync();

                    Variables.DnsCorrecto = true; //Dns correcto          
                }
            }
            catch
            {
                Variables.DnsCorrecto = false;//Cae a la excepción en caso de que no encuentra el DNS.  
            }

            return Variables.DnsCorrecto;
        }

        //Método para borrar la base de datos 
        private void BorrarBD()
        {
            Variables.ErrorTransDatos = 1;

            RepositorySatApp BBDD = new RepositorySatApp();

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
        }

        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }





    }
}
