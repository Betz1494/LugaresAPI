using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LugaresAPI.Repository.IRepository;

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

        [HttpGet]
        public IActionResult ObtenerTodosLugares()
        {
            var ObjLugares = repository.ObtenerLugares();

            return Ok(ObjLugares);
        }
    }
}
