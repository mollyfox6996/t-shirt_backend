using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Services.DTOs;
using Services.DTOs.OrderAggregate;
using Services.DTOs.CategoryDTOs;
using Services.DTOs.GenderDTOs;

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
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryForCreateDTO>().ReverseMap();
            CreateMap<Gender, GenderDTO>();
            CreateMap<CustomBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Like, LikeDTO>().ReverseMap();
            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(p => p.DeliveryMethod, p => p.MapFrom(s => s.DeliveryMethod.Name))
                .ForMember(p => p.Adress, p => p.MapFrom(s => s.Address))
                .ForMember(p => p.ShippingPrice, p => p.MapFrom(s => s.DeliveryMethod.Price))
                .ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDTO>().ReverseMap();
        }
    }
}
