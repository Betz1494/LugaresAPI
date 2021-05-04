using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LugaresAPI.Models.Actividad;

namespace LugaresAPI.Models.Entidades
{
    public class ActividadConsulta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public double Costo { get; set; }

        public TipoActividad Normal { get; set; }

        [Required]
        public int LugarId { get; set; }

   
        public LugarConsulta Lugar { get; set; }
    }
}
