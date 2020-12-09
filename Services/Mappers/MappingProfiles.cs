using AutoMapper;
using Domain.Entities;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TShirt, TShirtToReturnDTO>()
                .ForMember(p => p.Category, p => p.MapFrom(s => s.Category.Name))
                .ForMember(p => p.Gender, p => p.MapFrom(s => s.Gender.Name))
                .ForMember(p => p.AuthorName, p => p.MapFrom(s => s.User.DisplayName));
            CreateMap<Category, CategoryDTO>();
            CreateMap<Gender, GenderDTO>();
            CreateMap<CustomBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();

        }
    }
}
