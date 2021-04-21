using LugaresAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Repository.IRepository
{
    public interface ILugarRepository
    {
        ICollection<Lugar> ObtenerLugares();

        Lugar GetLugar(int IdLugar);

        bool ExisteLugar(string Nombre);

        bool ExisteLugar(int IdLugar);

        bool CrearLugar(Lugar lugar);

        bool ActualizaLugar(Lugar lugar);

        bool EliminaLugar(Lugar lugar);

        bool GuardarCambios();
    }
}
