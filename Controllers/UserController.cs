using System;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace UrlShortener.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserRepo _repository;

        //allows for dependancy to be injected
        public UserController(IUserRepo repo)
        {
            _repository = repo;
        }
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(string id)
        {
            var userItem = _repository.GetUserById(id);
            if (userItem == null)
            {
                return NotFound();
            }
            return Ok(userItem);
        }
        
    [HttpPost]
    public ActionResult<User>CreateUser(User user){
       
            if(_repository.IsTaken(user.Username)){
         ModelState.AddModelError("Username", "Username Name Already Exists.");
        
         return BadRequest(ModelState.Values);
            }
       


        string id=Guid.NewGuid().ToString();
        //extra check to ensure a duplicate Id is not generated
        while(_repository.GetUserById(id)!=null){
            id=Guid.NewGuid().ToString();
        }
        user.Id=id;

        _repository.CreateUser(user);
        _repository.SaveChanges();
    
    
        return CreatedAtRoute(nameof(GetUserById),new {id=user.Id},user);

    }
    //This route is only for testing purposes
   [HttpDelete]
     public ActionResult DeleteUsers(){
         

            _repository.DeleteUsers();
            _repository.SaveChanges();
            return NoContent();
        }
    
    }
  


}
