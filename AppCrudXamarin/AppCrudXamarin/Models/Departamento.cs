using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCrudXamarin.Models
{
    public class Departamento
    {
        [JsonProperty("numero")]
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public string Localidad { get; set; }
    }
}
