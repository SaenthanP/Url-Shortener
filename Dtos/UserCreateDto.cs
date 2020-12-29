using System.ComponentModel.DataAnnotations;

namespace UrlShortener
{
    public class UserCreateDto
    {
           
        [Required]
        [MinLength(8)]

        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}