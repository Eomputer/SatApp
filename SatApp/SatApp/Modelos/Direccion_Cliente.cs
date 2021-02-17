using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("DIRECCION_CLIENTE")]
    public class Direccion_Cliente
    {
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }
        public string CP { get; set; }
        public string Contacto { get; set; }
        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Comentario { get; set; }
        public bool Pago { get; set; }
        public bool Fusion { get; set; }
        public string Nombre { get; set; }
        public string Movil { get; set; }
        public bool Direccion_Albaran { get; set; }
        public string Via { get; set; }
        public string Numero { get; set; }
        public string Escalera { get; set; }
        public string Piso { get; set; }
        public string Puerta { get; set; }
        public string Nombre_Comercial { get; set; }
        public string Pais { get; set; }
        public int CodPoblacion { get; set; }
        public bool DestacarDireccion { get; set; }
        public int Distrito { get; set; }
        public string E_Mail { get; set; }
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

    }
}
