using SQLite;
using System;

namespace SatApp.Modelos
{
    [Table("GENERALES")]
    public class Generales
    {
        public int Codigo { get; set; }
        public string Empresa { get; set; }
        public string N_Comercial { get; set; }
        public string Direccion { get; set; }
        public string Municipio { get; set; }
        public string Codigo_Postal { get; set; }
        public string Provincia { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Fax { get; set; }
        public string NIF { get; set; }
        public string E_Mail { get; set; }
        public string Numero_Registro_Mercantil { get; set; }
        public int? Almacen { get; set; }
        public bool? Empresa_Principal { get; set; }
        public bool? Descatalogado { get; set; }
        public bool? PersonaFisica { get; set; }
        public bool? EsProfesional { get; set; }

        public Generales()
        {

        }
    }
}