using SatApp.Clases;
using SatApp.Dependencies;
using SatApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SatApp.Repository
{
    public class RepositorySatApp
    {
        private readonly SQLiteConnection cn;

        //Este repositorio nos permitirá almacenar los métodos o funciones que usaremos durante la aplicación.
        public RepositorySatApp()
        {
            try
            {
                //Invoca la funcionalidad nativa de la plataforma desde código compartido.
                cn = DependencyService.Get<IDataBase>().GetConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------------MÉTODOS--------------------------------

        #region Métodos Get
        //Verifica si hay partes Abiertos. Este método nos será útil para listar, los partes asignados al técnico y verificar a la hora de enviarlos y descargas de partes.
        public List<SAT> GetPartesAbiertos<T>(int codigo) where T : class
        {
            try
            {
                var list = cn.Query<SAT>("SELECT * FROM SAT WHERE Revisar = 0 And Realizado_por = ?", codigo).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Devuelve los datos de la Tabla a Consultar
        public List<T> GetAll<T>() where T : new()
        {
            try
            {
                List<T> list = cn.Table<T>().ToList();
                return list;
            }
            catch
            {
                List<T> item = null;
                return item;
            }
        }

        //Busca todos los partes asociados al personal
        public List<SAT> GetAllSAT(int personal)
        {
            try
            {
                var list = cn.Query<SAT>("SELECT * FROM SAT WHERE ATENDIDO_ENTRADA = ?", personal).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public T Get<T>(int codigo) where T : new()
        {
            try
            {
                var item = cn.Get<T>(codigo);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                T item = default;
                return item;
            }
        }

        //Devulve la direccion de cliente según el código.
        public Direccion_Cliente GetDireccionCliente(int codDireccion)
        {
            try
            {
                return cn.Table<Direccion_Cliente>().Where(c => c.ID == codDireccion).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Obtiene los datos de la tabla SAT referentes al número de parte que se le pasa
        public SAT GetSat(int parte)
        {
            try
            {
                return cn.Table<SAT>().Where(c => c.N_Parte == parte).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Obtiene la dirección del cliente.
        public List<Direccion_Cliente> GetDireccionesCliente(int cliente)
        {
            try
            {
                var list = cn.Table<Direccion_Cliente>().Where(c => c.Codigo == cliente).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SAT_Lineas> GetAllLineasParte(string parte)
        {
            try
            {
                var list = cn.Query<SAT_Lineas>("SELECT * FROM SAT_LINEAS WHERE N_PARTE = ? ORDER BY ORDEN_LINEA", parte).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Busca por código de barra.
        public Articulos GetArticuloPorCodBarras(string cod_barras)
        {
            try
            {
                return cn.Table<Articulos>().Where(c => c.Cod_Barras_Prov == cod_barras).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        //Busca tanto por referencia como por referencia de provvedor de la tabla articulo
        public Articulos GetArticuloReferencia(string cod_prov)
        {
            int referencia = Convert.ToInt32(cod_prov);
            try
            {
                string quitarEspacios = cod_prov.Replace(" ", "%");

                return cn.Query<Articulos>("SELECT * FROM ARTICULOS WHERE REFERENCIA = ? Or REFERENCIAPROV = ?", referencia, cod_prov).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulos> GetArticulosPorConsulta(string query)
        {
            string quitarEspacios = query.Replace(" ", "%");
            var list = cn.Query<Articulos>("SELECT * FROM ARTICULOS WHERE ARTICULO LIKE ?", "%" + quitarEspacios + "%");

            return list;
        }

        public List<Servicios> GetServiciosPorConsulta(string query)
        {
            string quitarEspacios = query.Replace(" ", "%");
            var list = cn.Query<Servicios>("SELECT * FROM SERVICIOS WHERE DESCRIPCION LIKE ?", "%" + quitarEspacios + "%");

            return list;
        }

        //Esta cosulta es para verificar si la tabla Articulos tiene Datos. En caso de estar vacía. Se hará una descarga completa, en caso contrario se hará una descarga parcial.
        public List<Articulos> GetListAticulos()
        {
            try
            {
                var cantidad = cn.Query<Articulos>("SELECT * FROM ARTICULOS");

                return cantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Busca todos los partes asociados al personal
        public List<SAT> SatAll()
        {
            try
            {
                var list = cn.Query<SAT>("SELECT * FROM SAT").ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public Forma_Pago ObtenerFormaPago(string codigo)
        {
            try
            {
                return cn.Table<Forma_Pago>().FirstOrDefault(c => c.Codigo == codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Provincias GetProvincias(string codigo)
        {
            try
            {
                return cn.Table<Provincias>().FirstOrDefault(c => c.Codigo == codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        //Esta en principio la pide
        //public string GetComercialCliente(int empresa, int cliente)
        //{
        //    string nombreComercial = "";
        //    try
        //    {
        //        var codigopersonal = cn.Table<ComercialCliente>().FirstOrDefault(c => c.Cliente == cliente && c.Empresa == empresa);

        //        if (codigopersonal != null)
        //        {
        //            var nombrepersonal = cn.Table<Personal>().FirstOrDefault(p => p.Codigo == codigopersonal.Personal);

        //            nombreComercial = nombrepersonal.Nombre + " " + nombrepersonal.Apellido1 + " " + nombrepersonal.Apellido2;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return nombreComercial;
        //}

        //Obtenemos el nombre del Técnico
        public string GetNombreTecnico(int codigo)
        {
            string tecnico = "";
            try
            {
                var person = cn.Table<Personal>().FirstOrDefault(personal => personal.Codigo == codigo);
                if (person != null)
                {
                    tecnico = person.Nombre + ' ' + person.Apellido1 + ' ' + person.Apellido2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tecnico;
        }

        #endregion

        #region Elimina Datos de las tablas Siguientes
        //Elimina todos los registros de la tabla SAT. Esto esta asociado al Login cuando descarga los partes.
        public void BorrarSat<T>()
        {
            cn.Query<SAT>("DELETE  FROM SAT ");
        }

        //Elimina todos los registros de la tabla SAT_Lineas         
        public void BorrarSatLineas<T>()
        {
            cn.Query<SAT_Lineas>("DELETE  FROM SAT_LINEAS");
        }

        //Elimina todos los registros de la tabla SAT_Equipo 
        public void BorrarSatEquipo<T>()
        {
            cn.Query<SAT_Equipo>("DELETE  FROM SAT_EQUIPO");
        }

        //Elimina todos los registros de la tabla CLIENTE 
        public void BorrarClientes<T>()
        {
            cn.Query<Cliente>("DELETE  FROM CLIENTE");
        }

        //Elimina todos los registros de la tabla CLIENTE_MAQUINA 
        public void BorrarClienteMaquinas<T>()
        {
            cn.Query<Cliente_Maquina>("DELETE  FROM CLIENTE_MAQUINA");
        }

        //Elimina todos los registros de la tabla DIRECCION_CLIENTE 
        public void BorrarDireccionCliente<T>()
        {
            cn.Query<Direccion_Cliente>("DELETE  FROM DIRECCION_CLIENTE");
        }

        //Elimina todos los registros de la tabla MAQUINAS        
        public void BorrarMaquinas<T>()
        {
            cn.Query<Maquinas>("DELETE  FROM MAQUINAS");
        }

        //Elimina todos los registros de la tabla PRECIOCLIENTE          
        //public void BorrarPrecioCliente<T>()
        //{
        //    cn.Query<PrecioCliente>("DELETE  FROM PRECIOCLIENTE");
        //}

        #endregion

        #region Métodos Borrar

        //Elimina las lineas del Parte, según el parte.
        public void BorrarSatLineas2<T>(int parte)
        {
            try
            {
                cn.Query<SAT_Lineas>("DELETE  FROM SAT_LINEAS Where N_Parte=?", parte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Elimina los registros de la tabla SAT, según parte 
        public void BorrarSatCerrado(List<SAT> model)
        {
            try
            {
                foreach (var s in model)
                {
                    cn.Query<SAT>("DELETE  FROM SAT WHERE N_PARTE = ?", s.N_Parte);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Elimina los registros de la tabla SAT_lineas que se le pasa
        public void BorrarLineasSatCerrado(List<SAT_Lineas> model)
        {
            try
            {
                foreach (var s in model)
                {
                    cn.Query<SAT_Lineas>("DELETE  FROM SAT_LINEAS WHERE N_PARTE = ?", s.N_Parte);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void EliminarLineasParte(string numParte)
        {
            try
            {
                cn.Query<SAT_Lineas>("DELETE FROM SAT_LINEAS WHERE N_PARTE = ?", numParte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // Eliminar lineas de parte
        public void EliminarLineaParte(SAT_Lineas linea)
        {
            try
            {
                cn.Query<SAT_Lineas>("DELETE FROM SAT_LINEAS WHERE N_PARTE = ? AND ORDEN_LINEA = ?", linea.N_Parte, linea.Orden_Linea);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        #endregion

        #region Métodos Reflexivos

        //Inserta o Actualiza dependiendo de si existe o no.
        public T InsertOrUpdate<T>(T model, int codigo) where T : class
        {
            try
            {
                var oldRecord = cn.Find<Conexiones>(codigo);
                if (oldRecord != null)
                {
                    cn.Update(model);
                }
                else
                {
                    cn.Insert(model);
                }

                return model;//Retorna el modelo
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;//En caso de error retorna el modelo.
            }
        }


        //Guarda cada una de las Tablas mientras se hace la descarga de Pirineos a SQLite
        public void SaveAsync<T>(List<T> list) where T : class
        {
            try
            {
                foreach (var record in list)
                {
                    cn.Insert(record);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Guarda los datos que vienen del filtro de los ténicos para las descarga de partes y sus lineas
        public void Save<T>(List<T> list) where T : class
        {
            try
            {
                foreach (var record in list)
                {
                    cn.Insert(record);
                    Console.WriteLine("Guardado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Recupera los partes que se han cerrado
        public List<SAT> SatCerrado<T>() where T : class
        {
            try
            {
                var list = cn.Query<SAT>("SELECT * FROM SAT WHERE REALIZADO = ? AND REVISAR=? AND FECHAENVIOAPP=?", 1, 0, "1900-12-30 00:00:00.000").ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        // Recupera los partes terminados pendientes de revisar
        public List<SAT> SatRevisar<T>() where T : class
        {
            try
            {
                var list = cn.Query<SAT>("SELECT * FROM SAT WHERE REALIZADO = ? AND REVISAR=? AND FECHAENVIOAPP=? ", 0, 1, "1900-12-30 00:00:00.000").ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        //Obtiene una lista con las lineas sat de los partes cerrados
        public List<SAT_Lineas> LineasSatCerrado<T>(int num) where T : class
        {
            try
            {
                var list = cn.Query<SAT_Lineas>("SELECT * FROM SAT_LINEAS WHERE N_PARTE = ?", num).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        //Obtiene una lista con las maquinas de los partes cerrados
        public List<SAT_Equipo> EquipoSatCerrado<T>(int maquina) where T : class
        {
            try
            {
                var list = cn.Query<SAT_Equipo>("SELECT * FROM SAT_EQUIPO WHERE CODIGO = ?", maquina).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        //Mientras la tabla SAT este llena es que hay partes por enviar
        public List<SAT> PartesSinEnviar<T>() where T : class
        {
            try
            {
                var list = cn.Query<SAT>("SELECT * FROM SAT ").ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Elimina el contenido completo de la tabla (Se usa para eliminar la BBDD, Un único proceso Reflextivo)        
        public void DeleteBBDD<T>() where T : class
        {
            try
            {
                cn.DeleteAll<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Actualizar registro de una clase
        public void Guardar<T>(T model)
        {
            try
            {
                cn.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        #endregion

        #region Cofigurar Base de Datos
        //Crea la base de datos
        public void CrearBBDD()
        {
            try
            {
                CrearTablasPorModelos();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        //Se crearán las tablas según el las Entidades que esten en la carpeta modelo
        public void CrearTablasPorModelos()
        {
            try
            {
                Funciones Funciones = new Funciones();
                Type[] typelist = Funciones.ObtenerModelos();
                foreach (var t in typelist)
                {
                    var T = Activator.CreateInstance(t);
                    cn.CreateTable(t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Cerramos la Conexión
        public void CerrarConexion()
        {
            try
            {
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Funciones Varias

        //Carga dentro de la Lista los partes asignados al técnico
        public DatosParte CargarDatosDeParte(SAT parteSAT)
        {
            try
            {
                Cliente cliente = Get<Cliente>(parteSAT.Cliente);

                var parte = new DatosParte
                {
                    Revisar = parteSAT.Revisar,
                    Realizado = parteSAT.Realizado,
                    Nombre_Comercial = cliente.Nombre_Comercial ?? ""
                };


                parte.N_Parte = parteSAT.N_Parte;

                if (parteSAT.Fecha_Entrada != null)
                    parte.Fecha = (DateTime)parteSAT.Fecha_Entrada;
                if (cliente != null)
                {
                    parte.CodigoCliente = cliente.Codigo;
                    parte.NombreCliente = cliente.Nombre;
                    parte.TieneRecargoEquivalencia = cliente.Recargo_Equivalencia;
                }
                if (parteSAT.Anomalia != null)
                    parte.AnomaliaParte = parteSAT.Anomalia;
                if (parteSAT.Solucion != null)
                    parte.Solucion = parteSAT.Solucion;
                if (parteSAT.Observaciones != null)
                    parte.Observaciones = parteSAT.Observaciones;
                if (parteSAT.Forma_Pago != null)
                    parte.FormaPago = parteSAT.Forma_Pago;
                if (parteSAT.Base_Total != 0)
                {
                    parte.Base1 = parteSAT.Base1;
                    parte.Base2 = parteSAT.Base2;
                    parte.Base3 = parteSAT.Base3;
                    parte.Base4 = parteSAT.Base4;
                    parte.Base5 = parteSAT.Base5;
                    parte.Base_Total = parteSAT.Base_Total;

                    parte.Iva1 = parteSAT.Iva1;
                    parte.Iva2 = parteSAT.Iva2;
                    parte.Iva3 = parteSAT.Iva3;
                    parte.Iva4 = parteSAT.Iva4;
                    parte.Iva5 = parteSAT.Iva5;
                    parte.Iva_Total = parteSAT.Iva_Total;

                    parte.Recargo_Equivalencia1 = parteSAT.Recargo_Equivalencia1;
                    parte.Recargo_Equivalencia2 = parteSAT.Recargo_Equivalencia2;
                    parte.Recargo_Equivalencia3 = parteSAT.Recargo_Equivalencia3;
                    parte.Recargo_Equivalencia4 = parteSAT.Recargo_Equivalencia4;
                    parte.Recargo_Equivalencia5 = parteSAT.Recargo_Equivalencia5;
                    parte.Recargo_Total = parteSAT.Recargo_Total;
                }

                if (parteSAT.Total != 0)
                {
                    parte.Total = parteSAT.Total;
                    parte.Revisar = parteSAT.Revisar;
                    parte.Realizado = parteSAT.Realizado;
                }


                if (parteSAT.Direccion != 0)
                {
                    Direccion_Cliente direccion = GetDireccionCliente(parteSAT.Direccion);

                    if (direccion != null)
                    {
                        parte.DireccionParte = direccion.Direccion;
                        parte.Poblacion = direccion.Poblacion;
                        parte.Provincia = Get<Provincias>(int.Parse(direccion.Provincia)).ToString();
                        parte.CodigoPostal = direccion.CP;
                        parte.TelefonoCliente = direccion.Telefono;
                    }
                }
                else
                {
                    if (cliente.Direccion != null)
                    {
                        parte.DireccionParte = cliente.Direccion;
                        if (cliente.Numero != "")
                        {
                            parte.DireccionParte = parte.DireccionParte + ", " + cliente.Numero;
                        }
                        parte.Poblacion = cliente.Municipio;
                        parte.Provincia = Get<Provincias>(int.Parse(cliente.Provincia)).ToString();
                        parte.CodigoPostal = cliente.C_P;
                    }
                    if (cliente.Telefono_1 != null)
                        parte.TelefonoCliente = cliente.Telefono_1;
                }

                parte.FechaEnvioApp = parteSAT.FechaEnvioApp;
                return parte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public Articulos ObtenerArticuloVarios()
        {
            Articulos articulo;
            try
            {
                var item = cn.Table<Valores_SAT>().FirstOrDefault();
                articulo = cn.Get<Articulos>(item.SAT_ArticuloVarios);
            }
            catch (Exception)
            {
                articulo = null;
            }
            return articulo;
        }

        //Devolver datos del servicio según su código.
        public Servicios ObtenerServicio(decimal referencia)
        {
            try
            {
                return cn.Table<Servicios>().Where(c => c.Referencia == referencia).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public T Insert<T>(T model)
        {
            try
            {
                cn.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Devolver datos del IVA.
        public Iva ObtenerRecargoIva(string codigo)
        {
            try
            {
                return cn.Table<Iva>().Where(c => c.Codigo == codigo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        //Este método verificará si existe la Tabla
        public int RecibeIva()
        {
            try
            {
                var lineas = cn.Query<int>("SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Iva' ").Count();
                return lineas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        #endregion


    }
}
