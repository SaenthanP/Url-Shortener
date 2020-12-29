using UrlShortner.Models;
namespace UrlShortner.Data
{
    public interface  IUserRepo
    {
        User GetUserById(int id);
        void CreateUser(User user);
        

    }
}