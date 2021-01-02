using AutoMapper;
using UrlShortener.Models;
using UrlShortener.Dtos;

namespace UrlShortener.Profiles
{
    public class LinksProfile:Profile
    {
            public LinksProfile(){
             
                CreateMap<Link,LinkCreateDto>();
                CreateMap<LinkCreateDto,Link>();


                CreateMap<LinkReadDto,Link>();
        CreateMap<Link,LinkReadDto>();



            }
    }
}
