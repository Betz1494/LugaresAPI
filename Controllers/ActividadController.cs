using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LugaresAPI.Repository.IRepository;
using LugaresAPI.Models.Entidades;
using LugaresAPI.Models;

namespace LugaresAPI.Controllers
{
    [Route("api/Actividad")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] //Lo tomen todos los metodos
    public class ActividadController : Controller
    {
        private IActividadRepository repository;
        private readonly IMapper mapper;

        public ActividadController(IActividadRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        /// <summary>
        /// Obtiene la Lista de las actividades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ActividadConsulta>))]
        public IActionResult ObtenerTodosActividades()
        {
            var ObjActividad = repository.ObtenerActividades();
            var consulta = new List<ActividadConsulta>();

            foreach (var dat in ObjActividad)
            {
                consulta.Add(mapper.Map<ActividadConsulta>(dat));
            }

            return Ok(consulta);
        }


        /// <summary>
        /// Obtiene Una Actividad dado un ID
        /// </summary>
        /// <param name="IdActividad">El Id de la Actividad</param>
        /// <returns></returns>
        [HttpGet("{IdActividad:int}", Name = "ObtieneActividad")]
        [ProducesResponseType(200, Type = typeof(ActividadConsulta))]
        [ProducesResponseType(404)] //return NotFound();
        [ProducesDefaultResponseType]
        public IActionResult ObtieneActividad(int IdActividad)
        {
            var actividad = repository.GetActividad(IdActividad);

            if (actividad == null)
            {
                return NotFound();
            }

            var consulta = mapper.Map<ActividadConsulta>(actividad);

            #region SinMapper
            /*var consulta = new LugarConsulta() 
              {
                 Creacion = lugar.Creacion,
                 Id = lugar.Id,
                 Nombre = lugar.Nombre,
                 Inauguracion = lugar.Inauguracion
              };*/
            #endregion

            return Ok(consulta);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ActividadConsulta))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreaActividad([FromBody] ActividadConsulta consulta)
        {
            //Validaciones
            if (consulta == null)
            {
                return BadRequest(ModelState);
            }

            if (repository.ExisteActividad(consulta.Nombre))
            {
                ModelState.AddModelError("", "El nombre de esta actividad ya Existe!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var datosActividad = mapper.Map<Actividad>(consulta);

            if (!repository.CrearActividad(datosActividad))
            {
                ModelState.AddModelError("", $"Ocurrio un ERROR al guardar la actividad {consulta.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("ObtieneActividad", new { IdActividad = consulta.Id }, datosActividad);
        }

        [HttpPatch("{IdActividad:int}", Name = "ActualizaActividad")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizaActividad(int IdActividad, [FromBody] ActividadConsulta consulta)
        {
            //Validaciones
            if (consulta == null || IdActividad != consulta.Id)
            {
                return BadRequest(ModelState);
            }

            var datosActividad = mapper.Map<Actividad>(consulta);

            if (!repository.ActualizaActividad(datosActividad))
            {
                ModelState.AddModelError("", $"Ocurrio un ERROR al Actualizar la actividad {consulta.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{IdActividad:int}", Name = "EliminaActividad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminaActividad(int IdActividad)
        {
            //Validaciones
            if (!repository.ExisteActividad(IdActividad))
            {
                return NotFound();
            }

            var Actividad = repository.GetActividad(IdActividad);

            if (!repository.EliminaActividad(Actividad))
            {
                ModelState.AddModelError("", $"Ocurrio un ERROR al Eliminar la Actividad {Actividad.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
