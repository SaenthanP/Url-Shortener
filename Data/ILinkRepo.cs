using UrlShortener.Models;

namespace UrlShortener.Data
{
    public interface ILinkRepo
    {
        
        bool SaveChanges();
        Link GetLinkById(string id);
        void Create(Link link);
        void DeleteLinks();
    }
}