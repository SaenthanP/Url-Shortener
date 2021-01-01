using UrlShortener.Models;

namespace UrlShortener.Data
{
    public interface ILinkRepo
    {
        
        bool SaveChanges();
        Link GetLinkById(string id);
        void CreateLink(Link link);
        void DeleteLink(Link link);
        Link GetLinkByUrlCode(string urlCode);

    }
}