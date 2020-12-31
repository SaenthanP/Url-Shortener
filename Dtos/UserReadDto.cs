using System.ComponentModel.DataAnnotations;

namespace UrlShortener
{
    public class UserReadDto
    {
           
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

   
    }
}