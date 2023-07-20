﻿using AutoMapper;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Configurations
{
    public class MapperConfig : Profile
    {
        /* Configuration that tells automapper that it is legal to convert from
         type A to type B. Reverse map basically says that the reverse of this
        is also legal */
        public MapperConfig()
        {
            CreateMap<Client, ClientViewModel>().ReverseMap();
            
        }
    }
}