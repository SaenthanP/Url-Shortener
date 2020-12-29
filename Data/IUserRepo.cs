using UrlShortener.Models;
namespace UrlShortener.Data
{
    public interface  IUserRepo
    {

        bool SaveChanges();
        User GetUserById(int id);
        void CreateUser(User user);
        bool IsTaken(string username);
        

    }
}