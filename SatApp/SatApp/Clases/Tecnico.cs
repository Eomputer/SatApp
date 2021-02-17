using Newtonsoft.Json;
using System;

namespace SatApp.Clases
{
    public class Tecnico
    {
        [JsonProperty("Codigo")]
        public int Codigo { get; set; }

        [JsonProperty("Usuario")]
        public string Usuario { get; set; }

        [JsonProperty("Contraseña")]
        public string Contraseña { get; set; }

        public Tecnico()
        {

        }
    }
}