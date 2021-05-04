using LugaresAPI.Data;
using LugaresAPI.Models;
using LugaresAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Repository
{
    public class ActividadRepository : IActividadRepository
    {
        private readonly ConexionBD conexion;

        public ActividadRepository(ConexionBD _conexion)
        {
            conexion = _conexion;
        }
       
        public bool CrearActividad(Actividad actividad)
        {
            conexion.Actividad.Add(actividad);

            return GuardarCambios();
        }

        public bool EliminaActividad(Actividad actividad)
        {
            conexion.Actividad.Remove(actividad);

            return GuardarCambios();
        }

        public Actividad GetActividad(int IdActividad)
        {
            return conexion.Actividad.Include(x => x.Lugar).FirstOrDefault(x => x.Id == IdActividad);
        }

        public ICollection<Actividad> ObtenerActividades()
        {
            return conexion.Actividad.Include(x => x.Lugar).OrderBy(x => x.Nombre).ToList();
        }

        public bool ExisteActividad(string Nombre)
        {
            bool existe = conexion.Actividad.Any(x => x.Nombre.ToLower().Trim() == Nombre.ToLower().Trim());

            return existe;
        }

        public bool ExisteActividad(int IdActividad)
        {
            return conexion.Actividad.Any(x => x.Id == IdActividad);
        }

        public bool GuardarCambios()
        {
            return conexion.SaveChanges() >= 0 ? true : false;
        }


        public bool ActualizaActividad(Actividad actividad)
        {
            conexion.Actividad.Update(actividad);
            return GuardarCambios();
        }

        public ICollection<Actividad> ObtieneActividadesxActividad(int IdLug)
        {
            return conexion.Actividad.Include(x => x.Lugar).Where(x => x.LugarId == IdLug).ToList();
        }
    }
}
