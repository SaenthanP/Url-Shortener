using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public interface ILinkRepo
    {
        
        bool SaveChanges();
        Link GetLinkById(string id);
        void CreateLink(Link link);
        void DeleteLink(Link link);
        void HangfireDeleteLink(string id);

        Link GetLinkByUrlCode(string urlCode);
        IEnumerable<Link>GetAllLinks(string userId);
    }
}