using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_de_Tareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now.AddDays(1);
        public bool Completada { get; set; } = false;
        public string Prioridad { get; set; } = "Media"; // Alta, Media, Baja
        public string Estado => Completada ? "Completada" : "Pendiente"; // propiedad calculada
    }
}
