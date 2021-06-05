using LugaresAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LugaresAPI.Repository.IRepository
{
    public interface IActividadRepository
    {
        ICollection<Actividad> ObtenerActividades();

        ICollection<Actividad> ObtieneActividadesxActividad(int IdActividad);

        Actividad GetActividad(int IdActividad);

        bool ExisteActividad(string Nombre);

        bool ExisteActividad(int IdActividad);

        bool CrearActividad(Actividad actividad);

        bool ActualizaActividad(Actividad actividad);

        bool EliminaActividad(Actividad actividad);

        bool GuardarCambios();
    }
}
