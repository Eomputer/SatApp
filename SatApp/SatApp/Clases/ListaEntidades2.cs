namespace SatApp.Clases
{
    public class ListaEntidades2
    {
        public int Value { get; set; }
        public string Name { get; set; }

        //Esta va a ser la lista de las Entidades que se van a descargar cada técnico con su configuración de forma completa para guardarlas en SQLite del Teléfono
        public enum Entidades
        {
            ListarClienteSat, ListarSatLineas, ListarSat, ListarSatEquipo, ListarClienteMaquinas, ListarDireccionCliente, ListarMaquinas, ListarPrecioCliente
        }
    }
}

