using LugaresAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Data
{
    public class ConexionBD: DbContext
    {
        public ConexionBD(DbContextOptions<ConexionBD> options) : base(options)
        {

        }

        public DbSet<Lugar> Lugar { get; set; }
        public DbSet<Actividad> Actividad { get; set; }

    }
}
