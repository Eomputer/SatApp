using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [PrimaryKey, NotNull]
        public int Codigo { get; set; }
        public string NIF { get; set; }
        public string Nombre { get; set; }
        public string Nombre_Comercial { get; set; }
        public string Via { get; set; }
        public string Numero { get; set; }
        public string Escalera { get; set; }
        public string Piso { get; set; }
        public string Puerta { get; set; }
        public string Provincia { get; set; }
        public int Pais { get; set; }
        public string Movil { get; set; }
        public string E_mail { get; set; }
        public string WWW { get; set; }
        public int Grupo { get; set; }
        public int Actividad { get; set; }
        public string Direccion { get; set; }
        public int Regimen_iva { get; set; }
        public string Forma_Pago { get; set; }
        public int Vencimiento { get; set; }
        public decimal RiesgoLimite { get; set; }
        public decimal DtoProntoPago { get; set; }
        public decimal RecargoFinanciero { get; set; }
        public bool Descatalogado { get; set; }
        public bool Recargo_Equivalencia { get; set; }
        public DateTime? Fecha_alta { get; set; }
        public DateTime? Fecha_baja { get; set; }
        public int Comercial { get; set; }
        public int Tarifa { get; set; }
        public bool Imprimir_Albaran_Sin_Valorar { get; set; }
        public bool Potencial { get; set; }
        public int Coste_Tarifa { get; set; }
        public decimal Porcentaje_Incremento { get; set; }
        public decimal DtoComercial { get; set; }
        public int IRPF { get; set; }
        public bool PersonaFisica { get; set; }
        public string Telefono_1 { get; set; }
        public string Telefono_2 { get; set; }
        public string Telefono_3 { get; set; }
        public string Fax { get; set; }
        public string C_P { get; set; }
        public string Municipio { get; set; }
        public bool DescansoLunes { get; set; }
        public bool DescansoMartes { get; set; }
        public bool DescansoMiercoles { get; set; }
        public bool DescansoJueves { get; set; }
        public bool DescansoViernes { get; set; }
        public bool DescansoSabado { get; set; }
        public bool DescansoDomingo { get; set; }
        public DateTime? HoraInicioLunes { get; set; }
        public DateTime? HoraInicioMartes { get; set; }
        public DateTime? HoraInicioMiercoles { get; set; }
        public DateTime? HoraInicioJueves { get; set; }
        public DateTime? HoraInicioViernes { get; set; }
        public DateTime? HoraInicioSabado { get; set; }
        public DateTime? HoraInicioDomingo { get; set; }
        public DateTime? HoraFinLunes { get; set; }
        public DateTime? HoraFinMartes { get; set; }
        public DateTime? HoraFinMiercoles { get; set; }
        public DateTime? HoraFinJueves { get; set; }
        public DateTime? HoraFinViernes { get; set; }
        public DateTime? HoraFinSabado { get; set; }
        public DateTime? HoraFinDomingo { get; set; }
        public bool VentaArticulosPrecioFijo { get; set; }
    }
}