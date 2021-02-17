using SQLite;

namespace SatApp.Modelos
{
    [Table("FORMA_PAGO")]
    public class Forma_Pago
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Recibo { get; set; }
        public decimal Subcuenta { get; set; }
        public bool Pagado { get; set; }
        public bool Pagare { get; set; }
        public bool Transferencia { get; set; }
        public bool Imprimir_Cuenta_Cliente { get; set; }
        public bool TodaslasCuentas { get; set; }
        public int Cuenta_Empresa { get; set; }
        public string Contrasena { get; set; }
        public bool SinDomiciliar { get; set; }
        public bool ImprimirVencimientos { get; set; }


        public Forma_Pago()
        {

        }
    }
}
