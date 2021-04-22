using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Models.Entidades
{
    public class LugarConsulta
    {      
        public int Id { get; set; }      
        public string Nombre { get; set; }     
        public string Localizacion { get; set; }
        public DateTime Creacion { get; set; }
        public byte[] Foto { get; set; }
        public DateTime Inauguracion { get; set; }
    }
}
