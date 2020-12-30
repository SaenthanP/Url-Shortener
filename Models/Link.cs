using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models
{
    public class Link{
    
        public string Id { get; set; }
        [Required]
        [MinLength(3)]
        // [MyValidator(ErrorMessage = "Username is taken")]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [NotMapped]
        [Required]

        [Compare(nameof(Password),ErrorMessage="No Match")]
        public string ConfirmPassword{get;set;}
  //may change to inlcude time as well      
[DataType(DataType.Date)]
[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ExpiryDate{get;set;}



    }
}