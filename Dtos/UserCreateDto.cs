using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos


{
    public class UserCreateDto
    {
           
        [Required]
        [MinLength(8)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage="No Match")]
        public string ConfirmPassword{get;set;}

    }
}