using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SatApp.Modelos
{
    [Table("REGIMEN_IVA")]
    public class Regimen_IVA
    {
        [PrimaryKey, NotNull]
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal SubCtaIvaCompras { get; set; }
        public decimal SubCtaIvaVentas { get; set; }
        public bool Es_Agrario { get; set; }

        public Regimen_IVA()
        {

        }
    }
}
