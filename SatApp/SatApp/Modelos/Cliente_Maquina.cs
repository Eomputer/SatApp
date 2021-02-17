using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("CLIENTE_MAQUINA")]
    public class Cliente_Maquina
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Direccion { get; set; }
        public int Maquina { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public bool Baja { get; set; }
        public DateTime? Fecha_Baja { get; set; }
        public string Ubicacion { get; set; }

        public Cliente_Maquina()
        {

        }
    }
}
