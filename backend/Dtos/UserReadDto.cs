using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos

{
    public class UserReadDto
    {
           
        // [Required(ErrorMessage="Enter Username")]
        public string Username { get; set; }
        // [Required(ErrorMessage="Enter Password")]
        public string Password { get; set; }

   
    }
}