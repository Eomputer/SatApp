using SQLite;

namespace SatApp.Modelos
{
    [Table("IVA")]
    public class Iva
    {
        public string Codigo { get; set; }
        public decimal Porcentaje { get; set; }
        public string Descripcion { get; set; }
        public decimal Recargo { get; set; }

    }
}
