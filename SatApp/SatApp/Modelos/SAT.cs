using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace SatApp.Modelos
{
    [Table("SAT")]
    public class SAT
    {

        [PrimaryKey, NotNull]
        public int N_Parte { get; set; }
        public int Empresa { get; set; }

        [ForeignKey(typeof(Cliente))]
        public int Cliente { get; set; }
        public int Direccion { get; set; }
        public DateTime Fecha_Entrada { get; set; } //Revisar este campo es Datetime?
        public DateTime Hora_Entrada { get; set; } //Revisar este campo también pasa nulo sin ?
        public string Fecha_Realizado { get; set; } //Revisar este campo es Datetime
        public int Equipo { get; set; }
        public int Marca { get; set; }
        public string N_Serie { get; set; }
        public string Anomalia { get; set; }
        public string Solucion { get; set; }
        public decimal Base1 { get; set; }
        public decimal Base2 { get; set; }
        public decimal Base3 { get; set; }
        public decimal Base4 { get; set; }
        public decimal Base5 { get; set; }
        public decimal Base_Total { get; set; }
        public decimal TantoIva1 { get; set; }
        public decimal TantoIva2 { get; set; }
        public decimal TantoIva3 { get; set; }
        public decimal TantoIva4 { get; set; }
        public decimal TantoIva5 { get; set; }
        public decimal Iva1 { get; set; }
        public decimal Iva2 { get; set; }
        public decimal Iva3 { get; set; }
        public decimal Iva4 { get; set; }
        public decimal Iva5 { get; set; }
        public decimal Iva_Total { get; set; }
        public decimal Tanto_Equivalencia1 { get; set; }
        public decimal Tanto_Equivalencia2 { get; set; }
        public decimal Tanto_Equivalencia3 { get; set; }
        public decimal Tanto_Equivalencia4 { get; set; }
        public decimal Tanto_Equivalencia5 { get; set; }
        public decimal Recargo_Equivalencia1 { get; set; }
        public decimal Recargo_Equivalencia2 { get; set; }
        public decimal Recargo_Equivalencia3 { get; set; }
        public decimal Recargo_Equivalencia4 { get; set; }
        public decimal Recargo_Equivalencia5 { get; set; }
        public decimal Recargo_Total { get; set; }
        public decimal Total { get; set; }
        public string Forma_Pago { get; set; }
        public int Vencimiento { get; set; }
        public bool Facturado { get; set; }
        public int N_Factura { get; set; }
        public int Atendido_Entrada { get; set; }
        public int Realizado_Por { get; set; }
        public string Lugar { get; set; }
        public bool Urgente { get; set; }
        public bool Realizado { get; set; }
        public bool? PendientePresupuesto { get; set; }
        public bool? PendienteRecambio { get; set; }
        public bool? SinReparar { get; set; }
        public bool? Garantia { get; set; }
        public bool? Mantenimiento { get; set; }
        public bool? Instalacion { get; set; }
        public bool? Impreso { get; set; }
        public bool? Presupuesto { get; set; }
        public string Modelo { get; set; }
        public bool Avisado { get; set; }
        public string Forma_Pago_Entrega_Cuenta { get; set; }
        public decimal Entrega_Cuenta { get; set; }
        public int Id_Cuota { get; set; }
        public int Almacen { get; set; }
        public int DesHacer { get; set; }
        public int Maquina { get; set; }
        public bool Equipo_Comercial { get; set; }
        public int Comercial { get; set; }
        public int Persona_Contacto { get; set; }
        public string Observaciones { get; set; }
        public DateTime Hora { get; set; }//Revisar este campo es Datetime
        public decimal ImporteArticulos { get; set; }
        public decimal ImporteServicios { get; set; }
        public decimal DtoPP { get; set; }
        public decimal ImporteDtoPP { get; set; }
        public decimal DtoCom { get; set; }
        public decimal ImporteDtoCom { get; set; }
        
        //Revisar estos dos campos pasan nulos
        public DateTime HoraInicioTarea { get; set; }
        public DateTime HoraFinTarea { get; set; }
        //

        public decimal TiempoTarea { get; set; }
        public string CoordenadaGeografica { get; set; }
        public bool Enviado { get; set; }
        public string N_Parte_Cli { get; set; }
        public string Documento { get; set; }
        public bool Revisar { get; set; }
        public int NumeroAlbaran { get; set; }
        public int ColetillaNumeroAlbaran { get; set; }
        public bool Entregado { get; set; }
        public string Fecha_Garantia { get; set; } //Revisar este campo es Datetime
        public int LineaNegocio { get; set; }
        public int N_Proyecto { get; set; }
        public int N_Cap { get; set; }
        public int N_SubCap { get; set; }
        public int N_Par { get; set; }
        public int Ruta { get; set; }
        public bool PendienteRepuesto { get; set; }
        public bool EnviadoAPP { get; set; }
        public bool RecibidoAPP { get; set; }
        public int PedidoClienteVinculado { get; set; }
        public int ReenviadoApp { get; set; }
        public bool? RepuestoRecibido { get; set; }
        public string FechaEnvioApp { get; set; }

        public SAT() { }

    }
}
