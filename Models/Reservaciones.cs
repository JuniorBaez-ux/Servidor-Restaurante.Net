using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Servidor.Models
{
    public class Reservaciones
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Cedula { get; set; }
        public string Mesa { get; set; }
    }
}
