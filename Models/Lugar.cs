using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Models
{
    public class Lugar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Localizacion { get; set; }
        public DateTime Creacion { get; set; }
        public byte[] Foto { get; set; }
        public DateTime Inauguracion { get; set; }
    }
}
