using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LugaresAPI.Models
{
    public class Actividad
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nombre { get; set; }

        public double Costo { get; set; }

        public enum TipoActividad { Basica, Normal, Premium, VIP }

        public TipoActividad Normal { get; set; }

        [Required]
        public int LugarId { get; set; }

        [ForeignKey("LugarId")]
        public Lugar Lugar { get; set; }

        public DateTime FechaCreacion { get; set; }

    }
}
