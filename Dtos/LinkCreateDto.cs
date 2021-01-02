using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Dtos
{
public class LinkCreateDto
{
   


        [Required]
        public string LongUrl{get;set;}

        //may change to inlcude time as well      
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm tt}")]
        public DateTime ExpiryDate { get; set; }
}
}