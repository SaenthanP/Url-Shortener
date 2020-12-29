using Microsoft.AspNetCore.Mvc;
using UrlShortner.Models;
using UrlShortner.Data;
namespace UrlShortner.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        
        private readonly IUserRepo _repository;

        //allows for dependancy to be injected
        public UserController(IUserRepo repo)
        {
            _repository=repo;
        }
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(int id)
        {
        var userItem=_repository.GetUserById(id);
        if(userItem==null){
            return NotFound();
        }
        return Ok(userItem);
        }
    }
}