using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("PERSONAL")]
    public class Personal
    {
        [PrimaryKey, NotNull]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Movil { get; set; }
        public bool Baja { get; set; }
        public decimal Com_min { get; set; }
        public string Contraseña { get; set; }
        public string Identificacion { get; set; }
        public string Iniciales { get; set; }
        public decimal PrecioHora { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string EmailLaboral { get; set; }

        public Personal() { }
    }
}
