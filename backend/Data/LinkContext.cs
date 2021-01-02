using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class LinkContext:DbContext
    {
        public LinkContext(DbContextOptions<LinkContext>opt):base(opt)
        {
            
        }
        public DbSet<Link>Link{get;set;}
    }
}