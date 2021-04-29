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
        public IActionResult ObtieneLugar(int IdLugar)
        {
            var lugar = repository.GetLugar(IdLugar);

            if (lugar == null)
            {
                return NotFound();
            }

            var consulta = mapper.Map<LugarConsulta>(lugar);

            return Ok(consulta);
        }

        [HttpPost]
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
