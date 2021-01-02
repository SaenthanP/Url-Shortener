using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models
{
    public class Link
    {

        public string Id { get; set; }
        public string UserId{get;set;}
        public string UrlCode{get;set;}
        public string ShortUrl{get;set;}
        public string JobId{get;set;}
        [Required]
        public string LongUrl{get;set;}

        //may change to inlcude time as well  
        
         [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm tt}")]
        public DateTime ExpiryDate { get; set; }
    }
}