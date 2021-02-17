using SQLite;

namespace SatApp.Modelos
{
    [Table("SAT_EQUIPO")]
    public class SAT_Equipo
    {
        public int Codigo { get; set; }
        [MaxLength(255)]
        public string Descripcion { get; set; }

    }
}
