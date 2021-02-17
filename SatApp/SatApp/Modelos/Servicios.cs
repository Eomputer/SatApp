using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("SERVICIOS")]
    public class Servicios
    {
        public decimal Referencia { get; set; }
        public decimal Precio { get; set; }
        public int Retencion { get; set; }
        public string Familia { get; set; }
        public string SubFamilia { get; set; }
        public decimal Precio2 { get; set; }
        public decimal Precio3 { get; set; }
        public bool Descatalogado { get; set; }
        public DateTime? Fecha_Descatalogado { get; set; }
        public string Observaciones { get; set; }
        public string Cod_Barras { get; set; }
        public string Cod_Barras_Prov { get; set; }
        public decimal Precio_coste { get; set; }
        public string ReferenciaTXT { get; set; }
        public string Descripcion { get; set; }
        public decimal IVA { get; set; }
        public decimal DtoPVP { get; set; }
        public decimal DtoPVD { get; set; }
        public decimal DtoPVD2 { get; set; }

        public Servicios()
        {

        }
    }
}
