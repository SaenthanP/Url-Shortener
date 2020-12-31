using System;
using System.Linq;
using AutoMapper;
using UrlShortener.Dtos;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class SqlLinkRepo : ILinkRepo
    {
        private readonly LinkContext _context;
        public SqlLinkRepo(LinkContext context)
        {
            _context = context;

        }
        public void CreateLink(Link link)
        {

            if (link == null)
            {
                throw new ArgumentNullException(nameof(link));

            }
            _context.Link.Add(link);
        }



        public void DeleteLinks()
        {
            throw new System.NotImplementedException();
        }

        public Link GetLinkById(string id)
        {
            return _context.Link.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);

        }

    }
}