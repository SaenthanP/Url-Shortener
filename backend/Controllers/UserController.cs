using System;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UrlShortener.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UrlShortener.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;

        //allows for dependancy to be injected
        public UserController(IUserRepo repo, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _repository = repo;
            _config = configuration;
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
        public ActionResult<User> CreateUser(UserCreateDto userCreateDto)
        {
            if(userCreateDto.Username==null||userCreateDto.Password==null||userCreateDto.ConfirmPassword==null){
                    ModelState.AddModelError("Empty entry", "Please enter all entries");

                return BadRequest(ModelState.Values);
            }

            var userModel = _mapper.Map<User>(userCreateDto);
            

            if (_repository.IsExists(userModel.Username))
            {
                ModelState.AddModelError("Username", "Username Name Already Exists.");

                return BadRequest(ModelState.Values);
            }

        if(userCreateDto.Password!=userCreateDto.ConfirmPassword){
             ModelState.AddModelError("Password", "Passwords do not match");

                return BadRequest(ModelState.Values);
        }

            string id = Guid.NewGuid().ToString();
            //extra check to ensure a duplicate Id is not generated
            while (_repository.GetUserById(id) != null)
            {
                id = Guid.NewGuid().ToString();
            }
            userModel.Id = id;

            userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);




            _repository.CreateUser(userModel);
            _repository.SaveChanges();

            var returnModel = _mapper.Map<UserReadDto>(userModel);
            return CreatedAtRoute(nameof(GetUserById), new { id = userModel.Id }, returnModel);

        }

        [HttpPost("authenticate")]
        public ActionResult<User> Login(UserReadDto user)
        {
            if(user.Username==null||user.Password==null){
                ModelState.AddModelError("Empty entry", "Please enter all entries");
                            return Unauthorized(ModelState.Values);

            }
            if (_repository.IsExists(user.Username))
            {
                var userToCheck = _repository.GetUserByUsername(user.Username);

                userToCheck = _mapper.Map<User>(userToCheck);
                if (BCrypt.Net.BCrypt.Verify(user.Password, userToCheck.Password))
                {

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = _config.GetSection("Key").Value;
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
        new Claim(ClaimTypes.Name,userToCheck.Id)
    }),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    var output = new
                    {
                        Token = tokenHandler.WriteToken(token),
                        User = user.Username
                    };


                    return Ok(output);
                }
            }

            ModelState.AddModelError("Invalid Login", "Username or Password entered Incorrectly");
            return Unauthorized(ModelState.Values);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("isAuthenticated")]
        public ActionResult <bool> isAuthenticated(){
            if(User.Identity.IsAuthenticated){
                return true;
            }
          return false;
        }
        [HttpDelete]
        public ActionResult DeleteUsers()
        {


            _repository.DeleteUsers();
            _repository.SaveChanges();
            return NoContent();
        }

    }



}
