﻿using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AutoMapper;

namespace AuthServer.Service;
 public class DtoMapper : Profile
{
    public DtoMapper() 
    {
        //  ProductDto nesnesini Produt nesnesine dönüştürmek için harita oluşturulur.
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<UserAppDto, AppUser>().ReverseMap();
    }

}
