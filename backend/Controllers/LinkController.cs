using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using shortid;
using shortid.Configuration;
using UrlShortener.Data;
using UrlShortener.Dtos;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly ILinkRepo _repository;
        private readonly IMapper _mapper;
        public LinkController(ILinkRepo repo, IMapper mapper,IConfiguration configuration)
        {
            _repository = repo;
            _mapper = mapper;
            _config=configuration;
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
        var options=new GenerationOptions{
            UseNumbers=true,
            Length=8,
            UseSpecialCharacters = false

        };
            string shortId=ShortId.Generate(options);
               while (_repository.GetLinkByUrlCode(shortId) != null)
            {
               shortId=ShortId.Generate(options);
            }
            linkModel.UrlCode=shortId;
            linkModel.ShortUrl = _config.GetSection("BaseUrl").Value+linkModel.UrlCode;
           //Checks the need for a permanent link or timed link
            if(DateTime.Compare(linkModel.ExpiryDate,DateTime.Now)>=0){
            //Stores job id to delete scheduled job if user manually wanted to delete the link
                var jobId= BackgroundJob.Schedule(()=>_repository.HangfireDeleteLink(linkModel.Id),linkModel.ExpiryDate);

              linkModel.JobId=jobId;

            }
           
            _repository.CreateLink(linkModel);
            _repository.SaveChanges();

            var returnModel = _mapper.Map<LinkReadDto>(linkModel);
            
            return CreatedAtRoute(nameof(GetLinkById), new { id = linkModel.Id }, returnModel);

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("api/authorized/links/{id}",Name = "GetLinkById")]

        [HttpGet]
        public ActionResult<LinkReadDto> GetLinkById([FromRoute]string id)
        {
            var linkItem = _repository.GetLinkById(id);
            if (linkItem != null)
            {
                return Ok(_mapper.Map<LinkReadDto>(linkItem));
            }
        return NotFound();
        }
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("api/authorized/links/all")]

        [HttpGet]
        public ActionResult<LinkReadDto> GetAllLinks()
        {
            var linkItems = _repository.GetAllLinks(User.Identity.Name);
          
                return Ok(_mapper.Map<IEnumerable<LinkReadDto>>(linkItems));
            
        
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("api/authorized/links/{id}")]
        [HttpDelete]
        public ActionResult DeleteLink(Guid id)
        {
            //Add to check to delete a job for hangfire
           
            var linkItem = _repository.GetLinkById(id.ToString());
            if (linkItem == null)
            {
                return NotFound();
            }else if(linkItem.UserId!=User.Identity.Name){
                return Unauthorized("You did not create this link");
            }
  if(linkItem.JobId!=null){
 BackgroundJob.Delete(linkItem.JobId);
  }          

    _repository.DeleteLink(linkItem);
    _repository.SaveChanges();

    return NoContent();

        }   
        


        [Route("")]
        [HttpGet("redirect/{urlCode}")]
        public ActionResult<Link>redirect(string urlCode){
            return Redirect(_repository.GetLinkByUrlCode(urlCode).LongUrl);
        }
    }
}