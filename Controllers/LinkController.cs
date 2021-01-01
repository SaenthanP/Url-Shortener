using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shortid;
using UrlShortener.Data;
using UrlShortener.Dtos;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    public class LinkController : ControllerBase
    {

        private readonly ILinkRepo _repository;
        private readonly IMapper _mapper;
        public LinkController(ILinkRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/authorized/links")]

        [HttpPost]
        public ActionResult<Link> CreateLink(LinkCreateDto linkCreateDto)
        {
            var linkModel = _mapper.Map<Link>(linkCreateDto);
            string id = Guid.NewGuid().ToString();
            //extra check to ensure a duplicate Id is not generated
            while (_repository.GetLinkById(id) != null)
            {
                id = Guid.NewGuid().ToString();
            }
            linkModel.Id = id;
       

            linkModel.UserId=User.Identity.Name;
            string shortId=ShortId.Generate();
               while (_repository.GetLinkByUrlCode(shortId) != null)
            {
               shortId=ShortId.Generate();
            }
            linkModel.UrlCode=shortId;
            linkModel.ShortUrl = "www.sample-url.com";
            _repository.CreateLink(linkModel);
            _repository.SaveChanges();

            var returnModel = _mapper.Map<LinkReadDto>(linkModel);


            return CreatedAtRoute(nameof(GetLinkById), new { id = linkModel.Id }, returnModel);

        }
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/authorized/links")]

        [HttpGet("{id}", Name = "GetLinkById")]
        public ActionResult<LinkReadDto> GetLinkById(string id)
        {
            var linkItem = _repository.GetLinkById(id);
            if (linkItem != null)
            {
                return Ok(_mapper.Map<LinkReadDto>(linkItem));
            }
        return NotFound();
        }
        [Route("")]

        [HttpGet("redirect/{urlCode}")]
        public ActionResult<Link>redirect(string urlCode){
// https://localhost:5001/redirect/23432
            return Redirect("http://www.google.com");
        }
    }
}