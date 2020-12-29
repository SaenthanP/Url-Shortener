using System;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Data;
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
        public ActionResult<User> GetUserById(int id)
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

        _repository.CreateUser(user);
        _repository.SaveChanges();
    
        return CreatedAtRoute(nameof(GetUserById),new {id=user.Id},user);
    }
   


    public bool IsUserNameExists(string Username){
        Console.WriteLine("reach");
        return _repository.IsTaken(Username);
    }
    }


}
