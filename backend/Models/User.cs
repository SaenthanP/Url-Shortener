using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Models
{
    public class User
    {
        public string Id { get; set; }
        [Required(ErrorMessage="Enter Username")]
        [MinLength(3)]
        // [MyValidator(ErrorMessage = "Username is taken")]
        public string Username { get; set; }
        [Required(ErrorMessage="Enter Password")]
        [MinLength(8)]
        public string Password { get; set; }
        // [NotMapped]
        // [Required]

        // [Compare(nameof(Password),ErrorMessage="No Match")]
        // public string ConfirmPassword{get;set;}

    }
}