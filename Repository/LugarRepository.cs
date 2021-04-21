using LugaresAPI.Data;
using LugaresAPI.Models;
using LugaresAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Repository
{
    public class LugarRepository : ILugarRepository
    {
        private readonly ConexionBD conexion;

        public LugarRepository(ConexionBD _conexion)
        {
            conexion = _conexion;
        }
       
        public bool CrearLugar(Lugar lugar)
        {
            conexion.Lugar.Add(lugar);

            return GuardarCambios();
        }

        public bool EliminaLugar(Lugar lugar)
        {
            conexion.Lugar.Remove(lugar);

            return GuardarCambios();
        }

        public Lugar GetLugar(int IdLugar)
        {
            return conexion.Lugar.FirstOrDefault(x => x.Id == IdLugar);
        }

        public ICollection<Lugar> ObtenerLugares()
        {
            return conexion.Lugar.OrderBy(x => x.Nombre).ToList();
        }

        public bool ExisteLugar(string Nombre)
        {
            bool existe = conexion.Lugar.Any(x => x.Nombre.ToLower().Trim() == Nombre.ToLower().Trim());

            return existe;
        }

        public bool ExisteLugar(int IdLugar)
        {
            return conexion.Lugar.Any(x => x.Id == IdLugar);
        }

        public bool GuardarCambios()
        {
            return conexion.SaveChanges() >= 0 ? true : false;
        }


        public bool ActualizaLugar(Lugar lugar)
        {
            conexion.Lugar.Update(lugar);
            return GuardarCambios();
        }

    }
}
