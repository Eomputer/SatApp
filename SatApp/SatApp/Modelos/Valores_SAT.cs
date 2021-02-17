using SQLite;

namespace SatApp.Modelos
{
    [Table("VALORES_SAT")]
    public class Valores_SAT
    {
        public int Id { get; set; }
        public int TarifaSAT { get; set; }
        public bool SAT_Garantia_Importe { get; set; }
        public int Clientes_Varios_SAT { get; set; }
        public decimal SAT_ArticuloVarios { get; set; }
        public bool SAT_RevisarParteAPP { get; set; }

        public Valores_SAT()
        {

        }

    }
}
