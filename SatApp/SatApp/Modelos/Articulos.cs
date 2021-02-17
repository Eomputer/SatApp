using SQLite;
using System;

namespace SatApp.Modelos
{
    public class Articulos
    {
        [PrimaryKey, NotNull]
        public decimal Referencia { get; set; }
        public string Cod_Barras { get; set; }
        public string Grupo { get; set; }
        public string Familia { get; set; }
        public int Marca { get; set; }
        public decimal PVP { get; set; }
        public decimal PVD { get; set; }
        public int Proveedor { get; set; }
        public decimal Precio_Tarifa { get; set; }
        public decimal Dto_Comercial { get; set; }
        public decimal Porcentaje_PVP { get; set; }
        public decimal Porcentaje_PVD { get; set; }
        public bool Sobre_Tarifa_PVP { get; set; }
        public bool Sobre_Tarifa_PVD { get; set; }
        public decimal Stock_Actual { get; set; }
        public string Observaciones { get; set; }
        public bool Descatalogado { get; set; }
        public bool Precio_Fijo { get; set; }
        public bool PVD_Fijo { get; set; }
        public decimal Dto_Linea { get; set; }
        public decimal Precio_Coste { get; set; }
        public string Foto { get; set; }
        public DateTime? Fecha_alta { get; set; }
        public string Talla { get; set; }
        public decimal PVD2 { get; set; }
        public bool PVD2_Fijo { get; set; }
        public bool Sobre_Tarifa_PVD2 { get; set; }
        public decimal Porcentaje_PVD2 { get; set; }
        public bool IncluirObservaciones { get; set; }
        public decimal Unidades_Caja { get; set; }
        public decimal Recargo { get; set; }
        public string WWW { get; set; }
        public decimal Dto1 { get; set; }
        public decimal Dto2 { get; set; }
        public decimal Dto3 { get; set; }
        public string ReferenciaTXT { get; set; }
        public string ReferenciaCliente { get; set; }
        public string SubTipo { get; set; }
        public string Cod_Barras_Prov { get; set; }
        public string Color { get; set; }
        public string ReferenciaProv { get; set; }
        public string Subfamilia { get; set; }
        public string Articulo { get; set; }
        public string DescripcionProv { get; set; }
        public string TipoIVA { get; set; }
        public decimal IVA { get; set; }
        public decimal ServicioAsociado { get; set; }
        public decimal DtoPVP { get; set; }
        public decimal PrecioNetoPVP { get; set; }
        public decimal DtoPVD { get; set; }
        public decimal PrecioNetoPVD { get; set; }
        public decimal DtoPVD2 { get; set; }
        public decimal PrecioNetoPVD2 { get; set; }
        public decimal PrecioServicioAsociado { get; set; }
    }
}