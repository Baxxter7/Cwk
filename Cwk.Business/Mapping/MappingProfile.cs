using AutoMapper;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;

namespace Cwk.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}