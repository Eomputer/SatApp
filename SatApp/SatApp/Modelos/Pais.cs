using SQLite;

namespace SatApp.Modelos
{
    [Table("PAIS")]
    public class Pais
    {
        public int Codigo_Pais { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }

    }
}

