using SQLite;

namespace SatApp.Modelos
{
    [Table("PROVINCIAS")]
    public class Provincias
    {
        [MaxLength(255)]
        public string Codigo { get; set; }
        [MaxLength(255)]
        public string Provincia { get; set; }
        public int Pais { get; set; }
        [MaxLength(255)]
        public string Observaciones { get; set; }

    }
}
