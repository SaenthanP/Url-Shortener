using System.Collections.Generic;
using UrlShortener.Models;
namespace UrlShortener.Data
{
    public interface  IUserRepo
    {

        bool SaveChanges();
        User GetUserById(string id);
        void CreateUser(User user);
        bool IsTaken(string username);
        void DeleteUsers();
    }
}