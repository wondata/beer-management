using AutoMapper;
using BeerManagement.Application.Models;

namespace BeerManagement.Application.Entities
{
    public class BeerMappingProfile : Profile
    {
        public BeerMappingProfile()
        {
            CreateMap<Beer, BeerEntity>();
            CreateMap<Rating, RatingEntity>();

            CreateMap<BeerEntity, Beer>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.EnteredAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<RatingEntity, Rating>()
                .ForMember(dest => dest.BeerId, opt => opt.MapFrom(src => src.BeerId))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.EnteredAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
