using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos


{
    public class UserCreateDto
    {
           
       
      
        public string Username { get; set; }
    
        public string Password { get; set; }

     
        public string ConfirmPassword{get;set;}

    }
}