namespace SatApp.Clases
{
    public class ListaEntidades
    {
        public int Value { get; set; }
        public string Name { get; set; }

        //Esta va a ser la lista de las Entidades que se van a descargar de forma completa para guardarlas en SQLite del Teléfono
        public enum Entidades
        {
            ListarGenerales, ListarFormaPago, ListarPersonal, ListarProvincias, ListarPaises, ListarIva, ListarRegimenIva, ListarValoresSat,
            ListarArticulos, ListarServicios, ListarClientes, ListarDireccionCliente, ListarClienteMaquina, ListarSatEquipo, ListarMaquinas,
            ListarSat, ListarSatLineas, UpdateRecibirLineas
        }
    }
}


