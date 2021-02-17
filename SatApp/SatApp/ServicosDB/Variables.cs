using SatApp.Clases;
using System;

namespace SatApp.ServiciosDB
{
    public static class Variables
    {
        public static int CodigoPersonal;
        public static string ClavePersonal;
        public static int CodigoParte;
        public static int ErrorLogin;
        public static int ErrorTransDatos;
        public static DatosParte DatosParte;
        public static bool Cargadatos;
        public static string Referencia;
        public static bool ConfImpresora;
        public static bool RevisaParte;
        public static decimal ArticuloVarios;
        public static bool BloqueaParte;
        public static string HoraInicioParte;
        public static bool BoolAcceso;


        //Estas variables nos van a servir para enviar a Pirineos según lo escogido por el Técnico
        public static bool OptRevisar;
        public static bool OptRealizado;
        public static bool OptPendienteRepuesto;
        public static bool OptPendientePresupuesto;
        public static bool OptPendienteRecambio;
        public static bool OptSinReparar;
        public static bool NoRealizado = false;//Será true en todos los casos que no sea realizado
        //-------------------------------------------------------------------------

        public static bool PrecioClienteFiltrado = false;

        //Variables para verificar la conexión a Internet
        public static bool ExisteInternet;

        //Como se va a ser una única descarga. En el Login que NO exista instancia de la BD, al ser null la descarga se hará Completa
        //Si existe una instancia de la BD la descarga es Parcial.
        public static bool DescargaTotal = false;
        public static bool DescargaParcial = false;

        //Url para conectarse al WebService.
        public static string ServerUrl;
        public static string Dns;//Con esto revisamos que la ip este correcto
        public static bool DnsCorrecto;//La ip o dns es correcto.
        public static string Servidor;
        public static int Puerto;
        public static string Entidad;



    }
}
