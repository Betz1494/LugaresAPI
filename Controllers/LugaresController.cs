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
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] //Lo tomen todos los metodos
    public class LugaresController : Controller
    {
        private ILugarRepository repository;
        private readonly IMapper mapper;

        public LugaresController(ILugarRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        /// <summary>
        /// Obtiene la Lista de todos los Lugares
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<LugarConsulta>))]
        public IActionResult ObtenerTodosLugares()
        {
            var ObjLugares = repository.ObtenerLugares();
            var consulta = new List<LugarConsulta>();

            foreach (var dat in ObjLugares)
            {
                consulta.Add(mapper.Map<LugarConsulta>(dat));
            }

            return Ok(consulta);
        }


        /// <summary>
        /// Obtiene Un Lugar dado un ID
        /// </summary>
        /// <param name="IdLugar">El Id del Lugar</param>
        /// <returns></returns>
        [HttpGet("{IdLugar:int}", Name = "ObtieneLugar")]
        [ProducesResponseType(200, Type = typeof(LugarConsulta))]
        [ProducesResponseType(404)] //return NotFound();
        [ProducesDefaultResponseType]
        public IActionResult ObtieneLugar(int IdLugar)
        {
            var lugar = repository.GetLugar(IdLugar);

            if (lugar == null)
            {
                return NotFound();
            }

            var consulta = mapper.Map<LugarConsulta>(lugar);

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
        [ProducesResponseType(201, Type = typeof(LugarConsulta))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreaLugar([FromBody] LugarConsulta consulta)
        {
            //Validaciones
            if (consulta == null)
            {
                return BadRequest(ModelState);
            }

            if (repository.ExisteLugar(consulta.Nombre))
            {
                ModelState.AddModelError("", "El nombre de este lugar ya Existe!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var datosLugar = mapper.Map<Lugar>(consulta);

            if (!repository.CrearLugar(datosLugar))
            {
                ModelState.AddModelError("", $"Ocurrio un ERROR al guardar el lugar {consulta.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("ObtieneLugar", new { IdLugar = consulta.Id }, datosLugar);
        }

        [HttpPatch("{IdLugar:int}", Name = "ActualizaLugar")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizaLugar(int IdLugar,[FromBody] LugarConsulta consulta)
        {
            //Validaciones
            if (consulta == null || IdLugar != consulta.Id)
            {
                return BadRequest(ModelState);
            }

            var datosLugar = mapper.Map<Lugar>(consulta);

            if (!repository.ActualizaLugar(datosLugar))
            {
                ModelState.AddModelError("", $"Ocurrio un ERROR al Actualizar el lugar {consulta.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{IdLugar:int}", Name = "EliminaLugar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminaLugar(int IdLugar)
        {
            //Validaciones
            if (!repository.ExisteLugar(IdLugar))
            {
                return NotFound();
            }

            var Lugar = repository.GetLugar(IdLugar);

            if (!repository.EliminaLugar(Lugar))
            {
                ModelState.AddModelError("", $"Ocurrio un ERROR al Eliminar el lugar {Lugar.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
