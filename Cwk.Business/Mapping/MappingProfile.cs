using AutoMapper;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;

namespace Cwk.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponseDto>();
        
        CreateMap<Amenity, AmenityResponseDto>();
        CreateMap<AddAmenityDto, Amenity>();
        CreateMap<UpdateAmenityDto, Amenity>();
        
        CreateMap<Space, SpaceDetailsDto>();
        CreateMap<AddSpaceDto, Space>();
        CreateMap<EditSpaceDto, Space>()
              .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

        CreateMap<Reservation, ReservationDetailsDto>();
        CreateMap<CreateReservationRequestDto, Reservation>();
        CreateMap<UpdateReservationRequestDto, Reservation>();
    }
}