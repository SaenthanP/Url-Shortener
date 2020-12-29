using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext>opt):base(opt)
        {
            
        }
        public DbSet<User>User{get;set;}
    }
}