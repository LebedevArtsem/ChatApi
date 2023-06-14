using AutoMapper;
using Chat.Api.Models;
using Chat.Domain;

namespace Chat.Api.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Friend, FriendModelResponse>();

    }
}

