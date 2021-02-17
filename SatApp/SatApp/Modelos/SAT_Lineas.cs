using SQLite;

namespace SatApp.Modelos
{
    [Table("SAT_LINEAS")]
    public class SAT_Lineas
    {
        public int N_Parte { get; set; }
        public int Orden_Linea { get; set; }
        public string TipoReferencia { get; set; }
        public decimal Referencia { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public decimal DTO { get; set; }
        public decimal Iva { get; set; }
        public decimal Almacen { get; set; }
        public bool Oferta { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public int N_Pedido { get; set; }
        public int Maquina { get; set; }
        public int IdLineaPedido { get; set; }
        public int Lote { get; set; }
        public int Pantilla { get; set; }
        public decimal Cantidad_Pantilla { get; set; }
        public int Orden_Pantilla { get; set; }
        public decimal CantidadCajas { get; set; }
        public string Capitulo { get; set; }
        public string SubCapitulo { get; set; }
        public int Conjunto { get; set; }
        public int OrdenConjunto { get; set; }
        public int UnidadPedido { get; set; }
        public decimal CantidadUnidadPedido { get; set; }
        public decimal PrecioCoste { get; set; }
        public decimal IDLineaAPP { get; set; }
        public decimal Recargo { get; set; }
        public int N_Proyecto { get; set; }
        public int N_Cap { get; set; }
        public int N_SubCap { get; set; }
        public int N_Par { get; set; }
        public decimal Retencion { get; set; }
        public string DepartamentoANA { get; set; }
        public string SeccionANA { get; set; }
        public string ProyectoANA { get; set; }
        public decimal ServicioAsociado { get; set; }
        public decimal ServicioAsociadoImporte { get; set; }
        public bool BebidaAzucarada { get; set; }
        public decimal LitroArticulo { get; set; }
        public bool VieneDeAPP { get; set; }


        public SAT_Lineas()
        {

        }
    }
}
