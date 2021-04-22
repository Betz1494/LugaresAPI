using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LugaresAPI.Models;
using LugaresAPI.Models.Entidades;

namespace LugaresAPI.Mapper
{
    public class LugaresMappings : Profile
    {
        public LugaresMappings()
        {
            CreateMap<Lugar, LugarConsulta>().ReverseMap();
        }

    }
}
