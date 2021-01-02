using AutoMapper;
using UrlShortener.Models;
using UrlShortener.Dtos;

namespace UrlShortener.Profiles
{
    public class UsersProfile:Profile
    {
            public UsersProfile(){
                CreateMap<User,UserCreateDto>();
                CreateMap<UserCreateDto,User>();
                CreateMap<UserReadDto,User>();
                CreateMap<User,UserReadDto>();




            }
    }
}
