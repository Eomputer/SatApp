using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("MAQUINAS")]
    public class Maquinas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Marca { get; set; }
        public int Equipo { get; set; }
        public string Modelo { get; set; }
        public string N_Serie { get; set; }
        public string Observaciones { get; set; }
        public bool Descatalogado { get; set; }
        public DateTime Fecha_Descatalogado { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha_Garantia { get; set; }
        public decimal PrecioHora { get; set; }
        public string Familia { get; set; }

    }
}
