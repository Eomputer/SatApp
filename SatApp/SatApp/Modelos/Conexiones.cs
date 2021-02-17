using SQLite;

namespace SatApp.Modelos
{
    [Table("CONEXIONES")]
    public class Conexiones
    {
        [PrimaryKey, NotNull]
        public int Codigo { get; set; }

        [MaxLength(255)]
        public string IP { get; set; }
        [MaxLength(255)]
        public string Puerto { get; set; }
        [MaxLength(255)]
        public string Dispositivo { get; set; }

        public bool Bloquea_Parte { get; set; }

        #region Constructor
        public Conexiones() { }
        #endregion



    }
}
