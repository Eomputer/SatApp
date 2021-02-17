using System;
using Xamarin.Essentials;

namespace SatApp.ServiciosDB
{
    public class AccessoInternet
    {
        //Método para verificar si existe conexión. Que le advierta al Usuario para posibles fallos de descarga de Base de datos o Partes
        public void VerificaConexion()
        {
            try
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    Variables.ExisteInternet = true;//Existe Conexión a Internet
                }
                else
                {
                    Variables.ExisteInternet = false;//No Existe Conexión a Internet
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
