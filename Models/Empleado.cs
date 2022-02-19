using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public string Cedula { get; set; }
        public DateTime Fecha_de_nacimiento { get; set; }
        public string Nombre_de_posicion { get; set; }
        public int DepartamentoId { get; set; }

    }
}
